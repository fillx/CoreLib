using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SceneManagement
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Game/Config/SceneConfig")]
    public class SceneConfig : ScriptableObject
    {
        public List<SceneConfigEntry> Scenes;

        private Dictionary<string, SceneConfigEntry> _sceneMap;

        public SceneConfigEntry GetScene(string id)
        {
            _sceneMap ??= BuildMap();
            return _sceneMap.TryGetValue(id, out var entry) ? entry : null;
        }

        private Dictionary<string, SceneConfigEntry> BuildMap()
        {
            var map = new Dictionary<string, SceneConfigEntry>();
            foreach (var scene in Scenes)
            {
                map[scene.SceneId] = scene;
            }
            return map;
        }
    }
    
    [System.Serializable]
    public class SceneConfigEntry
    {
        //TODO заменить string на enum
        public string SceneId;
        public AssetReference Key;     
    }
}