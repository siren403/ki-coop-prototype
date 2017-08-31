using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

namespace Contents4
{
    public class UIGetGiftPage : MonoBehaviour
    {


        public GameObject ImgSticker = null;

        // Use this for initialization
        void Start()
        {
            CDebug.Log("빵빠레효과");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HideSticker()
        {
            ImgSticker.SetActive(false);
        }

        public void ShowSticker()
        {
            ImgSticker.SetActive(true);
        }

        public void OnClickBtnGetGift()
        {

            ShowSticker();
        }
    }
}
