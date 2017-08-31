using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using DG.Tweening;
using UIComponent;

namespace MiniGame2
{
    public class UIMiniGame2 : MonoBehaviour
    {
        //*GameShop Item 버튼 리스트 */
        public List<Button> InstBtnPotList = new List<Button>();


        public GameObject InstPanelCoinShop = null;

        public void Initialize()
        {
            InstBtnPotList[0].onClick.AddListener(() => OnClickBtnPot(0));
            InstBtnPotList[1].onClick.AddListener(() => OnClickBtnPot(1));
            InstBtnPotList[2].onClick.AddListener(() => OnClickBtnPot(2));
            InstBtnPotList[3].onClick.AddListener(() => OnClickBtnPot(3));
        }

        public void OnClickBtnPot(int potNumber)
        {

        }



        public void OnClickBtnCoinShop()
        {
            InstPanelCoinShop.SetActive(true);
        }

    }
}