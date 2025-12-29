using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLib.Common;
using CoreLib.Configs.Providers.Interfaces;
using CoreLib.Configs.Service.Interfaces;
using UnityEngine;

namespace CoreLib.Configs.Service
{
    public abstract class ConfigServiceBase : IConfigService
    {
        private readonly IConfigProvider _provider;
        private readonly Dictionary<Type, ScriptableObject> _cache = new();

        protected ConfigServiceBase(IConfigProvider provider)
        {
            _provider = provider;
        }

        public async Task Init()
        {
            Dbg.Log("InitAsync started");
            foreach (var type in GetRequiredConfigs())
            {
                Dbg.Log($"Loading config: {type.Name}");
                var config = await _provider.LoadConfigAsync(type);
                if (!config)
                    Dbg.LogError($"Config not found: {type.Name}");

                _cache[type] = config;
            }

            Dbg.Log("All required configs loaded");
        }

        public bool TryGetConfig<T>(out T config) where T : ScriptableObject
        {
            var type = typeof(T);
            if (_cache.TryGetValue(type, out var obj) && obj is T typed)
            {
                config = typed;
                return true;
            }

            config = null;
            return false;
        }
        
        protected abstract Type[] GetRequiredConfigs();
    }
}