using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

namespace MiniGame1
{
    /**
     * ItemInfo Object(component), data class
     *
     * Information about the item.
     *
     * author  Byeong
     * date    2017-09-06
     */
    public class ItemInfo : MonoBehaviour
    {
        private int mId = 0;
        [SerializeField]
        private string mCategory = null;
        [SerializeField]
        private string mName = null;
        private int mPrice = 0;
        private bool mIsBuy = false;

        public int ItemID { get { return mId; } set { mId = value; } }
        public string ItemCategory { get { return mCategory; } set { mCategory = value; } }
        public string ItemName { get { return mName; } set { mName = value; } }
        public int ItemPrice { get { return mPrice; } set { mPrice = value; } }
        public bool ItemIsBuy { get { return mIsBuy; } set { mIsBuy = value; } }
    }
}


