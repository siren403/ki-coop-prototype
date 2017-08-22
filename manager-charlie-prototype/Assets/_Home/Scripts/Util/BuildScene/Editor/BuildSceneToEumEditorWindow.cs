using UnityEngine;
using UnityEditor;

namespace Util.BuildScene
{
    public class BuildSceneToEumEditorWindow : EditorWindow
    {
        [MenuItem("Tools/BuildSceneToEnum")]
        public static void OpenWindow()
        {
            var window = GetWindow<BuildSceneToEumEditorWindow>();

            window.Show();
        }

        private string EnumName = "BuildScene";
        private string Path;
        private string NameSpace = "SceneLoader";
        private string Prefix;

        void OnGUI()
        {
            EnumName = EditorGUILayout.TextField("EnumName", EnumName);
            NameSpace = EditorGUILayout.TextField("Namespace", NameSpace);
            Prefix = EditorGUILayout.TextField("Prefix", Prefix);

            EditorGUILayout.LabelField("Path", Path);
            if (GUILayout.Button("Select Path"))
            {
                Path = EditorUtility.OpenFolderPanel(null, Application.dataPath, null).Replace('/','\\');
            }

            GUI.enabled = !string.IsNullOrEmpty(EnumName);
            if (GUILayout.Button("Generate"))
            {
                BuildSceneToEnum.Generate(EnumName, Path, NameSpace, Prefix);
                AssetDatabase.Refresh();
            }
        }
    }
}