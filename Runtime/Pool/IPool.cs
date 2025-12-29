namespace CoreLib.Pool
{
    public interface IPool<T> where T : UnityEngine.Object
    {
        T Spawn(T prefab);
        void Despawn(T instance);
        void Clear();
    }
}