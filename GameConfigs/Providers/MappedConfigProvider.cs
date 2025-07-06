using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfigs
{
    public class MappedConfigProvider : IConfigProvider
    {
        private readonly Dictionary<Type, IConfigProvider> _bindings = new();

        public MappedConfigProvider Map<T>(IConfigProvider provider) where T : ScriptableObject
        {
            _bindings[typeof(T)] = provider;
            return this;
        }

        public bool CanProvide(Type type) => _bindings.ContainsKey(type);

        public async Task<ScriptableObject> LoadConfigAsync(Type type)
        {
            if (!_bindings.TryGetValue(type, out var provider))
                throw new Exception($"No provider mapped for config type {type.Name}");

            return await provider.LoadConfigAsync(type);
        }
    }
}