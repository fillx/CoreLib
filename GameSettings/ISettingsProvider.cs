using System.Threading.Tasks;
using UnityEngine;

namespace GameSettings
{
    public interface ISettingsProvider
    {
        Task<T> GetSettingsAsync<T>() where T : ScriptableObject;
    }
}