using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Morpeh.Game.Core.SceneManagement.Interfaces
{
    public interface ISceneService
    {
        Task LoadSceneAsync(string sceneId, LoadSceneMode mode = LoadSceneMode.Single,
            Action<float> onProgress = null);
    }
}