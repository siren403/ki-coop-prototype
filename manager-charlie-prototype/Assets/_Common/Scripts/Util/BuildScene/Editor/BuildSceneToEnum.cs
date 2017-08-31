using UnityEngine;
using Util;
#if UNITY_EDITOR
namespace Util.BuildScene
{
    public static class BuildSceneToEnum
    {
        public static void Generate(string name, string path, string nameSpace = null,string prefix = null)
        {
            UnityEditor.EditorBuildSettingsScene[] scenes = UnityEditor.EditorBuildSettings.scenes;
            if (scenes == null)
                return;

            string[] sceneNames = new string[scenes.Length];

            for (int i = 0; i < scenes.Length; i++)
            {
                string scnenName = System.IO.Path.GetFileName(scenes[i].path);
                scnenName = scnenName.Remove(scnenName.IndexOf('.'));
                sceneNames[i] = scnenName;
            }

            string generatePath = path;


            if (string.IsNullOrEmpty(generatePath))
                generatePath = Application.dataPath + string.Format("\\{0}.cs", name);
            else
                generatePath = string.Format("{0}\\{1}.cs", generatePath, name);

            StringArrayToEnum.Generate(sceneNames, name, generatePath, nameSpace, prefix);
        }
    }
}
#endif