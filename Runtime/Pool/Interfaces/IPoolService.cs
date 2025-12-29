using UnityEngine;

namespace CoreLib.Pool.Interfaces
{
    public interface IPoolService
    {
        GameObject Spawn(GameObject prefab);
        void Despawn(GameObject instance);
        void Clear();
    }
}