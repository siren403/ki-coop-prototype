using System.Collections;
using System.Collections.Generic;

namespace MiniGame1
{
    public class HaveItemInfo
    {
        private string mItemName;
        private bool mhaveItem;

        public string ItemName
        {
            get { return mItemName; }
            set { mItemName = value; }
        }

        public bool HaveItem
        {
            get { return mhaveItem; }
            set { mhaveItem = value; }
        }

        // initializer
        public HaveItemInfo(string name, bool have)
        {
            ItemName = name;
            HaveItem = have;
        }
    }
}
