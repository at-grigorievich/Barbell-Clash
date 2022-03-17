using UnityEditor;
using UnityEngine;

namespace ATG.LevelControl
{
#if UNITY_EDITOR
    [CustomEditor(typeof(ATGLevelLoader))]
    public class PrefsCleanerEditor : Editor
    {
        private bool _toggle;
        private int _neededLevel;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            var loader = target as ATGLevelLoader;
            
            GoTestLevel(loader);
            ClearPlayerPrefs(loader);
        }

        private void GoTestLevel(ATGLevelLoader loader)
        {
            GUILayout.Space(15);

            _toggle = EditorGUILayout.Toggle("NEED TEST LEVEL", _toggle);

            if (!_toggle) return;
            
            _neededLevel = EditorGUILayout.IntField("Needed Level", _neededLevel);

            GUILayout.Space(5);
            if (GUILayout.Button("Test level !"))
            {
                loader.GoToNeededLevel(_neededLevel);
                
                _toggle = false;
                _neededLevel = 0;
            }
        }
        private void ClearPlayerPrefs(ATGLevelLoader loader)
        {
            GUILayout.Space(25);
            
            if (GUILayout.Button("CLEAR ALL prefs"))
            {
                loader.ClearPlayerPrefs();
            }
        }
    }
#endif
}


