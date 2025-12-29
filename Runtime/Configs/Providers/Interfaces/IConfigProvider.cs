using System;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreLib.Configs.Providers.Interfaces
{
    public interface IConfigProvider
    { 
        bool CanProvide(Type type);
        Task<ScriptableObject> LoadConfigAsync(Type type);
    }
}