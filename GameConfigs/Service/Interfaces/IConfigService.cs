using System.Threading.Tasks;
using UnityEngine;

namespace GameConfigs
{
    public interface IConfigService
    {
        Task Init(); 
        bool TryGetConfig<T>(out T config) where T : ScriptableObject;
    }
}