using System;
using System.Threading.Tasks;
using Morpeh.Game.Core.GameConfigs.Providers.Interfaces;
using UnityEngine;

namespace Morpeh.Game.Core.GameConfigs.Providers
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