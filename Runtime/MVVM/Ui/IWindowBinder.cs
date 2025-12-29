namespace Morpeh.Game.Core.MVVM.Ui
{
    public interface IWindowBinder
    {
        void Bind(WindowViewModel viewModel);
        void Close();
    }
}