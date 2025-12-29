#if UNITY_EDITOR
using Morpeh.Game.Core.GameConfigs.Models;
using UnityEditor;
using UnityEngine;

namespace Morpeh.Game.Core.GameConfigs.Editor
{
    [CustomEditor(typeof(KeyConfig))]
    public class KeyConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var keyConfig = (KeyConfig)target;
            var entriesProp = serializedObject.FindProperty("entries");

            EditorGUILayout.PropertyField(entriesProp, true);

            if (GUILayout.Button("Обновить"))
            {
                foreach (var entry in keyConfig.entries)
                {
                    var guid = entry.configReference?.AssetGUID;
                    if (string.IsNullOrEmpty(guid)) continue;

                    var path = AssetDatabase.GUIDToAssetPath(guid);
                    var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                    if (asset != null)
                    {
                        entry.typeName = asset.GetType().Name;
                        Debug.Log($"[KeyConfigEditor] Set typeName: {entry.typeName} for {path}");
                    }
                    else
                    {
                        Debug.LogWarning($"[KeyConfigEditor] Could not load asset at path: {path}");
                    }
                }

                EditorUtility.SetDirty(keyConfig);
            }
        
        

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif