using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Morpeh.Game.Core.AssetLoader.Interfaces;
using Morpeh.Game.Core.Common.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Morpeh.Game.Core.AssetLoader.Services
{
    public class LoggingAssetService : IAssetService
    {
        private readonly IAssetService _inner;
        private const int WarningThreshold = 20;

        public LoggingAssetService(IAssetService inner)
        {
            _inner = inner;
        }

        public int ActiveAssetCount => _inner.ActiveAssetCount;

        private void LogCount()
        {
            Dbg.Log($"[AssetService] ActiveAssetCount = {_inner.ActiveAssetCount}");
            if (_inner.ActiveAssetCount > WarningThreshold)
            {
                Debug.LogWarning($"[AssetService] WARNING: ActiveAssetCount exceeded threshold ({WarningThreshold})");
            }
        }

        public async Task<T> LoadAsset<T>(AssetReference reference, CancellationToken token = default)
            where T : UnityEngine.Object
        {
            Debug.Log($"[AssetService] Loading asset: {reference.RuntimeKey}");
            var result = await _inner.LoadAsset<T>(reference, token);
            Debug.Log($"[AssetService] Loaded asset: {reference.RuntimeKey} => {result.name}");
            LogCount();
            return result;
        }

        public async Task Preload(IEnumerable<AssetReference> references, CancellationToken token = default)
        {
            Debug.Log($"[AssetService] Preloading {string.Join(", ", references)}");
            await _inner.Preload(references, token);
            LogCount();
        }

        public void Release(UnityEngine.Object asset)
        {
            Debug.Log($"[AssetService] Releasing asset: {asset.name}");
            _inner.Release(asset);
            LogCount();
        }
        
        public void Release(AssetReference asset)
        {
            Debug.Log($"[AssetService] Releasing asset: {asset.Asset.name}");
            _inner.Release(asset);
            LogCount();
        }

        public bool IsLoaded(AssetReference reference)
        {
            var loaded = _inner.IsLoaded(reference);
            Debug.Log($"[AssetService] IsLoaded({reference.RuntimeKey}) = {loaded}");
            return loaded;
        }

        public void Clear()
        {
            Debug.Log("[AssetService] Clearing cache");
            _inner.Clear();
            LogCount();
        }
        
        public void CancelAll() 
        {
            Debug.Log("[AssetService] Cancelling all loads");
            _inner.CancelAll();
        }
    }
}