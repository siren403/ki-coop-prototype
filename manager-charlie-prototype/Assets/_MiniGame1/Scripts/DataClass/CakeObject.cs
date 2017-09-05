using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame1
{
    /**
     * A cake object(component). Data Class
     *
     * @author  Seong
     * @date    2017-09-05
     */
    public class CakeObject : MonoBehaviour
    {
        public int ID { get { return mID; } set { mID = value; } }
        public string CATEGORY { get { return mCategory; } set { mCategory = value; } }
        
        [SerializeField]
        private int mID = 0;
        [SerializeField]
        private string mCategory = null;

        public CakeObject(int id, string category)
        {
            mID = id;
            mCategory = category;
        }
    }
}
