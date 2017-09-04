using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityQuickSheet;
namespace QuickSheet {
///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(MiniGame1))]
public class MiniGame1Editor : BaseExcelEditor<MiniGame1>
{	    
    public override bool Load()
    {
        MiniGame1 targetData = target as MiniGame1;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<MiniGame1Data>().ToArray();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
}