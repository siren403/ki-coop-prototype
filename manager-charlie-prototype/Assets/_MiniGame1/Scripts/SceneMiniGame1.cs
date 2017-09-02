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
        private List<HaveItemInfo> mHaveInfo = new List<HaveItemInfo>();
        private JsonData mJsonInfo;

        public int ItemCount
        {
            get
            {
                return mGameItem.dataArray.Length;
            }
        }        

        void Awake()
        {
            //mItemData = new List<QuickSheet.MiniGame1Data>();

            //SimpleIO io = new SimpleIO();

            //JsonData tempJson = new JsonData();
            //tempJson["ID"] = "Test";

            string jsonData = File.ReadAllText(Application.persistentDataPath + "/_MiniGame1/Resources/MiniGame1Info.json");

            mJsonInfo = JsonMapper.ToObject(jsonData);
            
            for(int i=0; i<mItemData.Count; i++)
            {
                CDebug.Log(mJsonInfo[i].ToString());
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
