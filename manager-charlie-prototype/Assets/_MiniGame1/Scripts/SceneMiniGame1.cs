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
        private QuickSheet.MiniGame1 mGameItem = null;

        [SerializeField]
        private ViewMiniGame1 mView = null;

        private List<QuickSheet.MiniGame1Data> mItemData = null;

        public int ItemCount
        {
            get
            {
                return mGameItem.dataArray.Length;
            }
        }

        void Awake()
        {
            mItemData = new List<QuickSheet.MiniGame1Data>();

            List<HaveItemInfo> mHaveInfo = new List<HaveItemInfo>();
            JsonData mJsonInfo;

            string jsonPath = File.ReadAllText(Application.persistentDataPath + "/_MiniGame1/Resources/MiniGame1Info.json");

            mJsonInfo = JsonMapper.ToObject(jsonPath);

            for(int i=0; i<mJsonInfo.Count; i++)
            {
                mHaveInfo.Add(new HaveItemInfo(mJsonInfo[i]["Name"].ToString(), mJsonInfo[i]["haveItem"].ToString()));
                //CDebug.Log(mHaveInfo[i].ItemName + " ? : " + mHaveInfo[i].HaveItem);
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
