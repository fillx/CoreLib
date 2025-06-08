using UnityEditor.PackageManager.UI;

namespace MVVM.Ui
{
    public interface IWindowBinder
    {
        void Bind(WindowViewModel viewModel);
        void Close();
    }
}