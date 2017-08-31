using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;
namespace QuickSheet {
///
/// !!! Machine generated code !!!
///
public class Contents2AssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/_Common/DataResources/Contents/QnAContents.xlsx";
    private static readonly string assetFilePath = "Assets/_Common/DataResources/Contents/Contents2.asset";
    private static readonly string sheetName = "Contents2";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            Contents2 data = (Contents2)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(Contents2));
            if (data == null) {
                data = ScriptableObject.CreateInstance<Contents2> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<Contents2Data>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<Contents2Data>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
}