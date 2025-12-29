using UnityEngine;

namespace Morpeh.Game.Core.Ui
{
    public interface IUiManager
    {
        void ShowLoading();
        void HideLoading();
        void AttachSceneUi(GameObject sceneUi);
        void ClearSceneUi();
    }
}