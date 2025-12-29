using UnityEngine;

namespace CoreLib.Ui
{
    public interface IUiManager
    {
        void ShowLoading();
        void HideLoading();
        void AttachSceneUi(GameObject sceneUi);
        void ClearSceneUi();
    }
}