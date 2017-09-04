using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame1
{
    public class CakeObject : MonoBehaviour
    {
        public int ID { get { return mID; } set { mID = value; } }
        [SerializeField]
        private int mID = 0;
    }
}
