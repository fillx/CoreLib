using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface IAssetProvider
    {
        Task<T> LoadAsync<T>(string key, IProgress<float> progress = null) where T : UnityEngine.Object;

        void Release(UnityEngine.Object asset);
    }
}