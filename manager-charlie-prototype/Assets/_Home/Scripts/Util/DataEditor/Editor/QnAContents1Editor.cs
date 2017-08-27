using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Util.DataEditor
{
    public class QnAContents1Editor : EditorWindow
    {
        [MenuItem("Tools/DataEditor/QnAContents1")]
        public static void OpenWindow()
        {
            var window = EditorWindow.GetWindow<QnAContents1Editor>();
        }
        private static Vector2 WindowSize = new Vector2(400, 600);

        [System.Serializable]
        private class Data
        {
            public int id;
            public int episodeID;
            public string category;
            public string correct;
        }

        private List<Data> DataTable = null;


        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("New"))
            {
                DataTable = new List<Data>();
            }
            if(GUILayout.Button("Load"))
            {
                Debug.Log("Load Data");
            }
            GUILayout.EndHorizontal();

            if (DataTable == null) return;

            if(GUILayout.Button("ToJson"))
            {
                Debug.Log(JsonUtility.ToJson(DataTable));
            }
            if (GUILayout.Button("Add"))
            {
                int tempID = 0;
                if(DataTable.Count > 0)
                {
                    tempID = DataTable.Last().id + 1;
                }
                DataTable.Add(new Data() { id = tempID });
                return;
            }
            foreach (var row in DataTable)
            {
                GUILayout.BeginHorizontal();
                row.id = EditorGUILayout.IntField("ID", row.id);
                row.episodeID = EditorGUILayout.IntField("EpisodeID", row.episodeID);
                row.category = EditorGUILayout.TextField("Category", row.category);
                row.correct = EditorGUILayout.TextField("Correct", row.correct);
                GUILayout.EndHorizontal();
            }
        }

    }
}
