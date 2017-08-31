using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.Data
{
    [System.Obsolete]
    public static class TableFactory 
    {
        public static QuickSheet.Contents1 LoadContents1Table()
        {
            QuickSheet.Contents1 table = null;
            table = Resources.Load<QuickSheet.Contents1>("QuickSheet/Contents1");
            return table;
        }
        public static QuickSheet.Contents2 LoadContents2Table()
        {
            QuickSheet.Contents2 table = null;
            table = Resources.Load<QuickSheet.Contents2>("QuickSheet/Contents2");
            return table;
        }
        public static QuickSheet.Contents3 LoadContents3Table()
        {
            QuickSheet.Contents3 table = null;
            table = Resources.Load<QuickSheet.Contents3>("QuickSheet/Contents3");
            return table;
        }
    }
}
