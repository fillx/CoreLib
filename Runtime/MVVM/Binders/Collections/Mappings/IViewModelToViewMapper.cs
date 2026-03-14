using System.Threading.Tasks;

namespace MVVM.Binders.Collections.Mappings
{
    public interface IViewModelToViewMapper
    {
        public void Init();
        public View GetPrefab(IViewModel viewModel);
        public Task<View> GetPrefabAsync(IViewModel viewModel);
    }
}