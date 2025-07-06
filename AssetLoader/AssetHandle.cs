using System;

namespace Assets
{
    public class AssetHandle<T> : IDisposable where T : UnityEngine.Object
    {
        public string Key { get; }
        public T Asset { get; }
        private readonly IAssetService _service;

        public AssetHandle(string key, T asset, IAssetService service)
        {
            Key = key;
            Asset = asset;
            _service = service;
        }

        public void Dispose()
        {
            _service.Release(Asset);
        }
    }

}