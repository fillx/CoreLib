using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameConfigs
{
    public interface IConfigProvider
    { 
        bool CanProvide(Type type);
        Task<ScriptableObject> LoadConfigAsync(Type type);
    }
}