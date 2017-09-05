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
                    mCakeObjects.Add(ItemSheet.dataArray[indexer].ID, new CakeObject(
                                                                                     ItemSheet.dataArray[indexer].ID,
                                                                                     ItemSheet.dataArray[indexer].Category, 
                                                                                     ItemSheet.dataArray[indexer].Name,
                                                                                     ItemSheet.dataArray[indexer].Price,
                                                                                     ItemSheet.dataArray[indexer].Isbuy));
                    CDebug.Log(ItemSheet.dataArray[indexer].ID);
                }
                else
                {
                    CDebug.LogError("Contains " + com.ID);
                }

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
