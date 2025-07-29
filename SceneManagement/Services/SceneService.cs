using System;
using System.Threading.Tasks;
using SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public class SceneService : ISceneService
    {
        private readonly SceneConfig config;

        public SceneService(SceneConfig config)
        {
            this.config = config;
        }
        public async Task LoadSceneAsync(string sceneId, LoadSceneMode mode = LoadSceneMode.Single, Action<float> onProgress = null)
        {
            SceneConfigEntry descriptor = config.GetScene(sceneId);
            if (descriptor == null)
                throw new Exception($"SceneId {sceneId} not found in config");

            var key = descriptor.Key;
            
            var handle = Addressables.LoadSceneAsync(key, mode);
            while (!handle.IsDone)
            {
                onProgress?.Invoke(handle.PercentComplete);
                await Task.Yield();
            }
            
            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new Exception($"Failed to load addressable scene: {key.RuntimeKey}");
        }
    }
}