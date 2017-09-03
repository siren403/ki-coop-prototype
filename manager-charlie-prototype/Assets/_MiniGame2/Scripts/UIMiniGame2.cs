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

        public GameObject InstPanelTitle;

        public GameObject InstPanelCoinShop = null;

        //*물뿌리개*/
        public GameObject InstImgWater;

        //*물뿌리개*/
        public GameObject InstImgFertilizer;

        public Vector3 ImgWaterPosition;

        public Button InstBtnExit;

        public void Start()
        {
            InstBtnExit.onClick.AddListener(() => OnClickExit());
            ImgWaterPosition = InstImgWater.transform.position;

            InstBtnPotList[0].onClick.AddListener(() => OnClickBtnPot(0));
            InstBtnPotList[1].onClick.AddListener(() => OnClickBtnPot(1));
            InstBtnPotList[2].onClick.AddListener(() => OnClickBtnPot(2));
            InstBtnPotList[3].onClick.AddListener(() => OnClickBtnPot(3));

            TitleFade();
        }
        void OnClickExit()
        {
            CDebug.Log("나가기 버튼 누르면 데이터 저장이 되고 씬 변경");
            SceneMiniGame2.instance.SaveJsonData();
        }

        void TitleFade()
        {
            InstPanelTitle.GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
            InstPanelTitle.SetActive(false));
        }
        public void OnClickBtnPot(int potNumber)
        {
            //* pot 번호 호출*/
            CDebug.Log(potNumber + "번 화분이 클릭 됨");
            SceneMiniGame2.instance.OnClickBtnPot(potNumber);

            //* 물주기 */
            if (SceneMiniGame2.instance.PotList[potNumber].IsEmpty == true)
            {
                CDebug.Log("비어있다");
            }
            else
            {
                InstImgWater.transform.DOMove(InstBtnPotList[potNumber].transform.position, 1).OnComplete(() =>
            InstImgWaterCallBack(potNumber));
            }
        }
        void InstImgWaterCallBack(int potNumber)
        {
            CDebug.Log("물 뿌려주기 시작!");
            SceneMiniGame2.instance.WaterTheFlower(potNumber);
            InstImgWater.transform.DOMove(ImgWaterPosition ,0);
        }

        public void OnClickBtnCoinShop()
        {
            InstPanelCoinShop.SetActive(true);
        }


    }
}