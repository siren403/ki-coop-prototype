using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame1
{
    /**
     * A cake object. Data Class
     *
     * @author  Seong
     * @date    2017-09-05
     */
    public class CakeObject : MonoBehaviour
    {
        public int ID { get { return mID; } set { mID = value; } }
        public string CATEGORY { get { return mCategory; } set { mCategory = value; } }
        public string NAME { get { return mName; } set { mName = value; } }
        public int PRICE { get { return mPrice; } set { mPrice = value; } }
        public bool MISBUY { get { return mIsBuy; } set { mIsBuy = value; } }
        
        private int mID = 0;
        private string mCategory = null;
        private string mName = null;
        private int mPrice = 0;
        private bool mIsBuy = false;

        public CakeObject(int id, string category, string name, int price, bool buy)
        {
            mID = id;
            mCategory = category;
            mName = name;
            mPrice = price;
            mIsBuy = buy;
        }
    }
}
