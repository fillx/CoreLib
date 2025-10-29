
using UnityEngine;

namespace Pool
{
    public interface IPooled
    {
        GameObject Prefab { get; set; }
    }
}