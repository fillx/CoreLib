using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tools;
using UnityEngine;

namespace GameConfigs
{
    public abstract class ConfigServiceBase : IConfigService
    {
        private readonly IConfigProvider _provider;
        private readonly Dictionary<Type, ScriptableObject> _cache = new();

        protected ConfigServiceBase(IConfigProvider provider)
        {
            _provider = provider;
        }

        public async Task InitAsync()
        {
            Dbg.Log("InitAsync started");
            foreach (var type in GetRequiredConfigs())
            {
                Dbg.Log($"Loading config: {type.Name}");
                var config = await _provider.LoadConfigAsync(type);
                if (!config)
                    throw new Exception($"Failed to load config of type {type.Name}");

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

        // Определяет список конфигов, которые должны быть загружены при запуске (InitAsync).
        // Если тип не указан в этом списке, он не будет загружен автоматически.
        // TODO: добавить ленивую загрузку конфигов по запросу через GetOrLoadConfigAsync.
        protected abstract Type[] GetRequiredConfigs();
    }
}