using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class Item
    {
        [SerializeField]
        private int mId;
        public int Id
        {
            get { return mId; }
            set { mId = Id; }
        }

        [SerializeField]
        private int mPrice;
        public int Price
        {
            get { return mPrice; }
            set { mPrice = Price; }
        }

        public enum ItemType
        {
            hat = 0,
            Glasses = 1,
            Dress = 2,
            None = 3,
        }

        public Vector2[] pos = { new Vector2(10, 10), new Vector2(20, 20), new Vector2(30, 30) };

        public ItemType ItemKind;

        public GameObject Image = null;

        bool isBought = false;

    }
}