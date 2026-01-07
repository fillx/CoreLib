using System.Threading.Tasks;
using UnityEngine;

namespace CoreLib.Configs.Service.Interfaces
{
    public interface IConfigService
    {
        Task Init();
        bool TryGetConfig<T>(out T config) where T : ScriptableObject;
    }
}