using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Util;


namespace Contents4
{
    public class UIAlarmSelectPage : MonoBehaviour
    {


        private bool mIsBtnStart = false;

        private SimpleTimer Timer = SimpleTimer.Create();

        [SerializeField]
        private float mCheckTime = 3.0f;

        public bool IsBtnStart
        {
            get
            {
                return mIsBtnStart;
            }
            set
            {
                mIsBtnStart = value;
            }
        }

        public GameObject PanelLock = null;

        private bool mIsBtnGiftLock = true;

        // Use this for initialization
        void Start()
        {
            Timer.Start();
        }

        // Update is called once per frame
        void Update()
        {
            Timer.Update();

            if (Timer.Check(mCheckTime))
            {
                IsBtnStart = true;
                HidePanelLock();
            }
        }

        public void HidePanelLock()
        {
            PanelLock.SetActive(false);
        }

        public void ShowPanelLock()
        {
            PanelLock.SetActive(true);
        }

        public void OnClickStartAlarm()
        {
            CDebug.Log("OnClick StartAlarm");

            IsBtnStart = true;
            HidePanelLock();
        }

        public void OnClickGetGift()
        {
            CDebug.Log("OnClick GetGift");

        }

    }
}
