using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Morpeh.Game.Core.GameConfigs.Models
{
    [CreateAssetMenu(fileName = "KeyConfig", menuName = "Game/Config/KeyConfig")]
    public class KeyConfig : ScriptableObject
    {
        public List<ConfigKeyEntry> entries = new();
    }

    [Serializable]
    public class ConfigKeyEntry
    {
        [HideInInspector] public string typeName;

        [SerializeField]
        public AssetReference configReference;
    }
}