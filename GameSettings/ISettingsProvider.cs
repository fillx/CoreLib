using System.Threading.Tasks;
using UnityEngine;

namespace GameConfig
{
    public interface ISettingsProvider
    {
        Task<T> GetSettingsAsync<T>() where T : ScriptableObject;
    }
}