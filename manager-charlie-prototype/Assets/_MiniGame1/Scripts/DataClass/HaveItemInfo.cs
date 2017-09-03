using System;
using System.Collections;
using System.Collections.Generic;

namespace MiniGame1
{
    public class HaveItemInfo
    {
        private string mItemName;
        private string mhaveItem;

        public string ItemName
        {
            get { return mItemName; }
            set { mItemName = value; }
        }

        public string HaveItem
        {
            get { return mhaveItem; }
            set { mhaveItem = value; }
        }

        // initializer
        public HaveItemInfo(string name, string have)
        {
            ItemName = name;
            HaveItem = have;
        }
    }
}
