using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfigs
{ 
    public class ResourcesConfigProvider : IConfigProvider
    {
        public bool CanProvide(Type type)
        {
            string path = $"Configs/{type.Name}";
            return Resources.Load(path, type) != null;
        }

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            string path = $"Configs/{type.Name}";
            var config = Resources.Load(path, type) as ScriptableObject;
            return await Task.FromResult(config);
        }
    }
}