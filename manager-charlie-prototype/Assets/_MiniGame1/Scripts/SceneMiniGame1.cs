using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Contents.QnA;
using LitJson;
using System.Linq;
using Util;

namespace MiniGame1
{
    public class SceneMiniGame1 : MonoBehaviour
    {
        [SerializeField]
        private ViewMiniGame1 mView = null;

        public QuickSheet.MiniGame1 ItemSheet = null;

        private Dictionary<int, CakeObject> mCakeObjects = new Dictionary<int, CakeObject>();
        public Dictionary<int, ItemInfo> DicItemInfo = new Dictionary<int, ItemInfo>();

        // ItemSheet의 총 길이를 가져오는 Getter
        public int ItemCount
        {
            get
            {
                return ItemSheet.dataArray.Length;
            }
        }

        void Awake()
        {
            mCakeObjects.Clear();
            DicItemInfo.Clear();

            var components = mView.InstCake.GetComponentsInChildren<CakeObject>();

            int indexer = 0;

            // CakeObject 정보 초기화
            foreach (var com in components)
            {
                // Dictionary 초기화
                if (mCakeObjects.ContainsKey(com.ID) == false)
                {
                    mCakeObjects.Add(indexer + 1, components[indexer]);
                    //CDebug.Log(ItemSheet.dataArray[indexer].ID);
                }
                else
                {
                    CDebug.LogError("Contains " + com.ID);
                }

                mCakeObjects[indexer + 1].ID = indexer;

                indexer++;
            }
        }

        public void ItemInitializer()
        {
            var ItemComponents = mView.ItemList.GetComponentsInChildren<ItemInfo>();

            int indexer = 0;

            // 아이템 정보 초기화
            foreach (var itemcom in ItemComponents)
            {
                // Dictionary 초기화
                if (DicItemInfo.ContainsKey(ItemSheet.dataArray[indexer].ID) == false)
                {
                    DicItemInfo.Add(indexer, ItemComponents[indexer]);
                    CDebug.Log(ItemSheet.dataArray[indexer].ID);
                }
                else
                {
                    CDebug.LogError("Contains ");
                }

                DicItemInfo[indexer].ItemID = ItemSheet.dataArray[indexer].ID;
                DicItemInfo[indexer].ItemCategory = ItemSheet.dataArray[indexer].Category;
                DicItemInfo[indexer].ItemName = ItemSheet.dataArray[indexer].Name;
                DicItemInfo[indexer].ItemPrice = ItemSheet.dataArray[indexer].Price;
                DicItemInfo[indexer].ItemIsBuy = ItemSheet.dataArray[indexer].Isbuy;

                indexer++;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

//mCakeObjects.Add(ItemSheet.dataArray[indexer].ID, components[indexer]);