using System.Collections.Generic;
using CoreLib.Pool.Interfaces;
using UnityEngine;

namespace CoreLib.Pool.Service
{
    public class PoolService : IPoolService
    {
        private readonly Dictionary<GameObject, Queue<GameObject>> _pool = new();
        private readonly Transform _root;

        public PoolService(Transform root = null)
        {
            _root = root ?? CreateDefaultRoot();
        }

        public GameObject Spawn(GameObject prefab)
        {
            if (!_pool.TryGetValue(prefab, out var queue))
            {
                queue = new Queue<GameObject>();
                _pool[prefab] = queue;
            }

            GameObject instance;
            if (queue.Count > 0)
            {
                instance = queue.Dequeue();
                instance.SetActive(true);
            }
            else
            {
                instance = Object.Instantiate(prefab, _root);
            }

            // Автоматическое назначение ссылки на оригинальный префаб
            var pooled = instance.GetComponent<IPooled>();
            if (pooled != null) pooled.Prefab = prefab;

            return instance;
        }

        public void Despawn(GameObject instance)
        {
            instance.SetActive(false);
            instance.transform.SetParent(_root, false);

            var pooled = instance.GetComponent<IPooled>();
            var prefabId = pooled?.Prefab;

            if (prefabId != null)
            {
                if (!_pool.TryGetValue(prefabId, out var queue))
                {
                    queue = new Queue<GameObject>();
                    _pool[prefabId] = queue;
                }

                queue.Enqueue(instance);
            }
            else
            {
                Object.Destroy(instance); // fallback если без IPooled
            }
        }

        public void Clear()
        {
            foreach (var queue in _pool.Values)
            foreach (var obj in queue)
                Object.Destroy(obj);

            _pool.Clear();
        }

        private static Transform CreateDefaultRoot()
        {
            var go = new GameObject("[PoolRoot]");
            Object.DontDestroyOnLoad(go);
            return go.transform;
        }
    }
}