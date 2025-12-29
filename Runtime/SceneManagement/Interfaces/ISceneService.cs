using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace CoreLib.SceneManagement
{
    public interface ISceneService
    {
        Task LoadSceneAsync(string sceneId, LoadSceneMode mode = LoadSceneMode.Single,
            Action<float> onProgress = null);
    }
}