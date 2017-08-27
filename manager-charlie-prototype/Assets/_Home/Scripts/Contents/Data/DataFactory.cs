using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.Data
{
    public static class DataFactory 
    {
        public static QuickSheet.Contents1 LoadContents1Table()
        {
            QuickSheet.Contents1 table = null;
            table = Resources.Load<QuickSheet.Contents1>("QuickSheet/Contents1");
            return table;
        }
    }
}
