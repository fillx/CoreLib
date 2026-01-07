using System;
using System.Threading.Tasks;
using CoreLib.Configs.Providers.Interfaces;
using UnityEngine;

namespace CoreLib.Configs.Providers
{
    public class ResourcesConfigProvider : IConfigProvider
    {
        public bool CanProvide(Type type)
        {
            var path = $"Configs/{type.Name}";
            return Resources.Load(path, type) != null;
        }

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            var path = $"Configs/{type.Name}";
            var config = Resources.Load(path, type) as ScriptableObject;
            return await Task.FromResult(config);
        }
    }
}