using System;
using System.Collections.Generic;
using System.Linq;
using Morpeh.Game.Core.Common.Tools;

namespace Morpeh.Game.Core.DI
{
    public class DiContainer : IDisposable
    {
        private readonly DiContainer _parentContainer;
        private readonly Dictionary<(string, Type), DIEntry> _entriesMap = new();
        private readonly HashSet<(string, Type)> _resolutionsCache = new();

        public DiContainer(DiContainer parentContainer = null)
        {
            _parentContainer = parentContainer;
        }

        public DIEntry RegisterFactory<T>(Func<DiContainer, T> factory)
        {
            return RegisterFactory(null, factory);
        }

        public DIEntry RegisterFactory<T>(string tag, Func<DiContainer, T> factory)
        {
            var key = (tag, typeof(T));

            if (_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"DI: Factory with tag {key.Item1} and type {key.Item2.FullName} has already registered");
            }

            var diEntry = new DIEntry<T>(this, factory);

            _entriesMap[key] = diEntry;

            return diEntry;
        }

        public void RegisterInstance<T>(T instance)
        {
            RegisterInstance(null, instance);
        }

        public void RegisterInstance<T>(string tag, T instance)
        {
            var key = (tag, typeof(T));

            if (_entriesMap.ContainsKey(key))
            {
                throw new Exception(
                    $"DI: Instance with tag {key.Item1} and type {key.Item2.FullName} has already registered");
            }

            var diEntry = new DIEntry<T>(instance);

            _entriesMap[key] = diEntry;
        }

        public T Resolve<T>(string tag = null)
        {
            var key = (tag, typeof(T));

            if (_resolutionsCache.Contains(key))
            {
                Dbg.LogInfra($"DI: Cyclic dependency for tag {key.tag} and type {key.Item2.FullName}");
                throw new Exception($"DI: Cyclic dependency for tag {key.tag} and type {key.Item2.FullName}");
            }

            _resolutionsCache.Add(key);

            try
            {
                if (_entriesMap.TryGetValue(key, out var diEntry))
                {
                    return diEntry.Resolve<T>();
                }

                if (_parentContainer != null)
                {
                    return _parentContainer.Resolve<T>(tag);
                }
            }
            finally
            {
                _resolutionsCache.Remove(key);
            }

            Dbg.LogInfra($"Couldn't find dependency for tag {tag} and type {key.Item2.FullName}");
            throw new Exception($"Couldn't find dependency for tag {tag} and type {key.Item2.FullName}");
        }

        public void Dispose()
        {
            var entries = _entriesMap.Values;
            
            //TODO Удалить после релиза
            var typeNames = _entriesMap.Keys.Select(k => k.Item2.Name);
            Dbg.LogInfra($"Container with: {string.Join(", ", typeNames)} Disposed");

            foreach (var entry in entries)
            {
                entry.Dispose();
            }
        }
    }
}