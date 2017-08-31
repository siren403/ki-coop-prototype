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
[CustomEditor(typeof(Contents3))]
public class Contents3Editor : BaseExcelEditor<Contents3>
{	    
    public override bool Load()
    {
        Contents3 targetData = target as Contents3;

        string path = targetData.SheetName;
        if (!File.Exists(path))
            return false;

        string sheet = targetData.WorksheetName;

        ExcelQuery query = new ExcelQuery(path, sheet);
        if (query != null && query.IsValid())
        {
            targetData.dataArray = query.Deserialize<Contents3Data>().ToArray();
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            return true;
        }
        else
            return false;
    }
}
}