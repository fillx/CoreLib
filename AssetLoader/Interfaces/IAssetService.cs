using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AssetLoader
{
    public interface IAssetService
    {
        Task<T> LoadAsset<T>(AssetReference reference, CancellationToken token = default)
            where T : UnityEngine.Object;
        
        Task Preload(IEnumerable<AssetReference> references, CancellationToken token = default);

        //TODO В будущем необходимо научится выгружать ассеты которые на уровне
        void Release(UnityEngine.Object asset);
        void Release(AssetReference reference);
        bool IsLoaded(AssetReference reference);
        void Clear();
        int ActiveAssetCount { get; }
        void CancelAll();
    }
}