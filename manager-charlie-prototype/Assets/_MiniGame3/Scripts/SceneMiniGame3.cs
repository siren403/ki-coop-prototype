using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MiniGame3
{
    public class SceneMiniGame3 : MonoBehaviour
    {

        public ViewMiniGame3 InstViewMiniGame3 = null;

        public List<Item> ItemList = new List<Item>();

        public Item[] ItemData = new Item[4];

        public UnitInfo UnitInfo = new UnitInfo();
        void Start()
        {
            for(int ti = 0; ti< ItemData.Length;ti++)
            {

                ItemData[ti].Id = ti;
                ItemData[ti].Price = ti * 100;
            }

            ItemData[0].ItemKind = Item.ItemType.hat;
            ItemData[1].ItemKind = Item.ItemType.Glasses;
            ItemData[2].ItemKind = Item.ItemType.Dress;
            ItemData[3].ItemKind = Item.ItemType.None;

            InstViewMiniGame3.SetScene(this);

            GetItemData();
        }


        void Update()
        {

        }

        public void GetItemData()
        {
            for (int ti = 0; ti < ItemData.Length;ti++ )
            {
                ItemList.Add(ItemData[ti]);
            }
        }


    }
}

