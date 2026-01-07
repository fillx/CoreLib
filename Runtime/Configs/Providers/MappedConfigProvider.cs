using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLib.Common;
using CoreLib.Configs.Providers.Interfaces;
using UnityEngine;

namespace CoreLib.Configs.Providers
{
    public class MappedConfigProvider : IConfigProvider
    {
        private readonly Dictionary<Type, IConfigProvider> _bindings = new();

        public MappedConfigProvider Map<T>(IConfigProvider provider) where T : ScriptableObject
        {
            _bindings[typeof(T)] = provider;
            return this;
        }

        public bool CanProvide(Type type)
        {
            return _bindings.ContainsKey(type);
        }

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            if (!_bindings.TryGetValue(type, out var provider))
            {
                Dbg.LogError($"Config not found: {type.Name}");
                return null;
            }

            return await provider.LoadConfigAsync(type);
        }
    }
}