using System;
using System.Threading.Tasks;
using CoreLib.Common;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace CoreLib.SceneManagement
{
    public class SceneService : ISceneService
    {
        private readonly SceneConfig config;

        public SceneService(SceneConfig config)
        {
            this.config = config;
        }

        public async Task LoadSceneAsync(string sceneId, LoadSceneMode mode = LoadSceneMode.Single,
            Action<float> onProgress = null)
        {


            SceneConfigEntry descriptor = config.GetScene(sceneId);
            if (descriptor == null)
            {
                Dbg.LogError("Scene not found: " + sceneId);
                return;
            }
            
#if UNITY_EDITOR
            if (SceneManager.GetActiveScene().name == descriptor.Key.editorAsset.name)
                return;
#endif

            var key = descriptor.Key;

            var handle = Addressables.LoadSceneAsync(key, mode);
            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await Task.Yield();
            }

            if (handle.Status != AsyncOperationStatus.Succeeded)
                Dbg.LogError($"Failed to load scene {descriptor.Key}");
        }
    }
}