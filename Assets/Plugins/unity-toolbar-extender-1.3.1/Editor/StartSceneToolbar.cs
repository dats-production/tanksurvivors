using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace UnityToolbarExtender
{
    [InitializeOnLoad]
    public static class StartSceneToolbar
    {
//-------------------------SET-START-SCENE--------------------------
        [MenuItem("Assets/Set Start Scene", false, 1000000)]
        private static void SetStartSceneMenuItem()
        {
            var selection = Selection.activeObject;
            var scene = selection as SceneAsset;

            EditorPrefs.SetString("StartScenePath", AssetDatabase.GetAssetPath(scene));

            Debug.Log($"Scene {scene.name} was chosen as the starting one");
        }

        [MenuItem("Assets/Set Start Scene", true)]
        private static bool SetStartSceneValidation() => Selection.activeObject != null &&
                                                         Selection.activeObject is SceneAsset;
//------------------------------------------------------------------
        
        
        private static SceneAsset StartScene
        {
            get
            {
                if (!EditorPrefs.HasKey("StartScenePath")) return null;
                var path = EditorPrefs.GetString("StartScenePath");
                return AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
            }
        }

        static StartSceneToolbar()
        {
            ToolbarExtender.LeftToolbarGUI.Remove(OnToolbarGUI);
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
            
            EditorApplication.playModeStateChanged += ResetStartScene;
        }

        private static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            if (StartScene != null)
            {
                if (!GUILayout.Button($"{StartScene.name}",
                    new GUIStyle(GUI.skin.button), GUILayout.Height(22))) return;

                EditorSceneManager.playModeStartScene = StartScene;
                EditorApplication.isPlaying = true;
            }
            else
            {
                GUILayout.Label("Please specify the starting scene");
            }
        }

        private static void ResetStartScene(PlayModeStateChange state)
        {
            if (EditorSceneManager.playModeStartScene != StartScene ||
                state != PlayModeStateChange.ExitingPlayMode) return;
            
            EditorSceneManager.playModeStartScene = null;
        }
    }
}
