using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLib.Configs.Providers.Interfaces;
using UnityEngine;

namespace CoreLib.Configs.Providers
{
    public class CompositeConfigProvider : IConfigProvider
    {
        private readonly List<IConfigProvider> _providers;

        public CompositeConfigProvider(params IConfigProvider[] providers)
        {
            _providers = providers.ToList();
        }

        public bool CanProvide(Type type)
        {
            return _providers.Any(p => p.CanProvide(type));
        }

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            foreach (var provider in _providers)
            {
                if (provider.CanProvide(type))
                {
                    var config = await provider.LoadConfigAsync(type);
                    if (config != null)
                        return config;
                }
            }

            throw new Exception($"No provider found for config type {type.Name}");
        }
    }
}