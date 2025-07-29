using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetLoader
{
    public class AddressablesAssetService : IAssetService
    {
        private readonly Dictionary<AssetReference, AsyncOperationHandle<UnityEngine.Object>> _cache = new();
        private readonly CancellationManager _cancellationManager = new();
        private const int RetryCount = 2;
        private const int RetryDelayMs = 100;

        public int ActiveAssetCount => _cache.Count;

        public async Task<T> LoadAsset<T>(AssetReference reference, CancellationToken token = default)
            where T : UnityEngine.Object
        {
            if (_cache.TryGetValue(reference, out var handle))
            {
                return handle.Result as T;
            }

            for (int attempt = 0; attempt <= RetryCount; attempt++)
            {
                handle = reference.LoadAssetAsync<UnityEngine.Object>();
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                _cancellationManager.Track(linkedCts);

                try
                {
                    await handle.Task;
                    linkedCts.Token.ThrowIfCancellationRequested();
                    _cache[reference] = handle;
                    return handle.Result as T;
                }
                catch (OperationCanceledException)
                {
                    if (handle.IsValid()) Addressables.Release(handle);
                    throw;
                }
                catch
                {
                    if (handle.IsValid()) Addressables.Release(handle);
                    if (attempt == RetryCount) throw;
                    await Task.Delay(RetryDelayMs, token);
                }
                finally
                {
                    linkedCts.Dispose();
                }
            }

            return null;
        }


        public async Task Preload(IEnumerable<AssetReference> references, CancellationToken token = default)
        {
            foreach (var reference in references)
            {
                if (_cache.ContainsKey(reference)) continue;

                for (int attempt = 0; attempt <= RetryCount; attempt++)
                {
                    var handle = reference.LoadAssetAsync<UnityEngine.Object>();
                    var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token);
                    _cancellationManager.Track(linkedCts);

                    try
                    {
                        await handle.Task;
                        linkedCts.Token.ThrowIfCancellationRequested();
                        _cache[reference] = handle;
                        break;
                    }
                    catch (OperationCanceledException)
                    {
                        if (handle.IsValid()) Addressables.Release(handle);
                        throw;
                    }
                    catch
                    {
                        if (handle.IsValid()) Addressables.Release(handle);
                        if (attempt == RetryCount) break;
                        await Task.Delay(RetryDelayMs, token);
                    }
                    finally
                    {
                        linkedCts.Dispose();
                    }
                }
            }
        }

        public void Release(UnityEngine.Object asset)
        {
            foreach (var kvp in _cache)
            {
                if (kvp.Value.Result == asset && kvp.Value.IsValid())
                {
                    Addressables.Release(kvp.Value);
                    _cache.Remove(kvp.Key);
                    break;
                }
            }
        }

        public void Release(AssetReference reference)
        {
            if (_cache.TryGetValue(reference, out var handle) && handle.IsValid())
            {
                Addressables.Release(handle);
                _cache.Remove(reference);
            }
        }

        public bool IsLoaded(AssetReference reference) => _cache.ContainsKey(reference);

        public void Clear()
        {
            foreach (var handle in _cache.Values)
            {
                if (handle.IsValid()) Addressables.Release(handle);
            }

            _cache.Clear();
            _cancellationManager.CancelAll();
            _cancellationManager.ClearCompleted();
        }

        public void CancelAll()
        {
            _cancellationManager.CancelAll();
        }
    }
}