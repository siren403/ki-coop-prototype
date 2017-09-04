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

        [SerializeField]
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
