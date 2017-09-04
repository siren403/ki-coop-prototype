using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;
namespace QuickSheet {
///
/// !!! Machine generated code !!!
///
public class MiniGame1AssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/_Common/DataResources/MiniGame/MiniGameItem.xlsx";
    private static readonly string assetFilePath = "Assets/_Common/DataResources/MiniGame/MiniGame1.asset";
    private static readonly string sheetName = "MiniGame1";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            MiniGame1 data = (MiniGame1)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(MiniGame1));
            if (data == null) {
                data = ScriptableObject.CreateInstance<MiniGame1> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<MiniGame1Data>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<MiniGame1Data>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
}