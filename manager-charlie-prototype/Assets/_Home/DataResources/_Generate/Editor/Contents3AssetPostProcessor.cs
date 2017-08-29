using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;
namespace QuickSheet {
///
/// !!! Machine generated code !!!
///
public class Contents3AssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/_Home/DataResources/Contents/QnAContents.xlsx";
    private static readonly string assetFilePath = "Assets/Resources/QuickSheet/Contents3.asset";
    private static readonly string sheetName = "Contents3";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            Contents3 data = (Contents3)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(Contents3));
            if (data == null) {
                data = ScriptableObject.CreateInstance<Contents3> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<Contents3Data>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<Contents3Data>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
}