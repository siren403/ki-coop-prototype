using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class Item : MonoBehaviour
    {
        [SerializeField]
        private int mId;
        public int Id
        {
            get
            {
                return mId;
            }
            set
            {
                mId = Id;
            }
        }
        [SerializeField]
        private int mPrice;
        public int Price
        {
            get
            {
                return mPrice;
            }
            set
            {
                mPrice = Price;
            }
        }

        public enum ItemType
        {
            hat = 0,
            Glasses = 1,
            Dress = 2,
            None = 3,
        }

        public ItemType ItemKind;

        public GameObject Image = null;


        
        void Start()
        {
           
        }


    }
}