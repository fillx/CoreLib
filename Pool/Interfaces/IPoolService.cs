using UnityEngine;

namespace Pool
{
    public interface IPoolService
    {
        GameObject Spawn(GameObject prefab);
        void Despawn(GameObject instance);
        void Clear();
    }
}