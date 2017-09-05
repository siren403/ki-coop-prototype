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

        public int ItemCount
        {
            get
            {
                return ItemSheet.dataArray.Length;
            }
        }

        void Awake()
        {
            CDebug.Log("before : " + mCakeObjects.Count);

            var components = mView.InstCake.GetComponentsInChildren<CakeObject>();

            int indexer = 0;

            foreach (var com in components)
            {
                if (mCakeObjects.ContainsKey(com.ID) == false)
                {
                    mCakeObjects.Add(ItemSheet.dataArray[indexer].ID, components[indexer]);
                    CDebug.Log(ItemSheet.dataArray[indexer].ID);
                }
                else
                {
                    CDebug.LogError("Contains " + com.ID);
                }

                int tmpIdx = ItemSheet.dataArray[indexer].ID;

                mCakeObjects[tmpIdx].ID = ItemSheet.dataArray[indexer].ID;
                mCakeObjects[tmpIdx].CATEGORY = ItemSheet.dataArray[indexer].Category;
                mCakeObjects[tmpIdx].NAME = ItemSheet.dataArray[indexer].Name;
                mCakeObjects[tmpIdx].PRICE = ItemSheet.dataArray[indexer].Price;
                mCakeObjects[tmpIdx].MISBUY = ItemSheet.dataArray[indexer].Isbuy;

                indexer++;
            }

            for(int i=0; i<components.Length; i++)
            {
                CDebug.Log(mCakeObjects[i+1].ID);
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
