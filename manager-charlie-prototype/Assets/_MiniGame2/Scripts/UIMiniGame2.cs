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

        private SceneMiniGame2 mScene = null;

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

        public Image InstImgTouch;

        public Transform InstImgSeed;

        public void Start()
        {
            InstImgTouch.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
            InstBtnExit.onClick.AddListener(() => OnClickExit());
            ImgWaterPosition = InstImgWater.transform.position;

            InstBtnPotList[0].onClick.AddListener(() => OnClickBtnPot(0));
            InstBtnPotList[1].onClick.AddListener(() => OnClickBtnPot(1));
            InstBtnPotList[2].onClick.AddListener(() => OnClickBtnPot(2));
            InstBtnPotList[3].onClick.AddListener(() => OnClickBtnPot(3));

            TitleFade();
        }


        public void SetScene(SceneMiniGame2 scene)
        {
            mScene = scene;
        }


        void OnClickExit()
        {
            CDebug.Log("나가기 버튼 누르면 데이터 저장이 되고 씬 변경");
            mScene.SaveJsonData();
        }

        void TitleFade()
        {
            InstPanelTitle.GetComponent<Image>().DOFade(0, 1).OnComplete(() =>
            InstPanelTitle.SetActive(false));
        }

        //*물뿌리개가 움직이고 콜백으로 물주기 함수 호출 */
        public void OnClickBtnPot(int potNumber)
        {
            //* pot 번호 호출*/
            CDebug.Log(potNumber + "번 화분이 클릭 됨");
            mScene.OnClickBtnPot(potNumber);

            //* 물주기 */
            if (mScene.PotList[potNumber].IsEmpty == true)
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
            mScene.WaterTheFlower(potNumber);
            InstImgWater.transform.DOMove(ImgWaterPosition, 0);
        }


        //*꽃이 생성되는(심기는) UI 보여주기 */
        public void ShowPlantFlower(int flowerNumber,int potNumber)
        {
            InstImgSeed.position = Vector2.one * 200;

            InstImgSeed.DOMove(InstBtnPotList[potNumber].transform.position, 1);
            InstImgSeed.DOScale(0,1)
                         .OnStart(() =>
                         {
                             InstImgSeed.gameObject.SetActive(true);
                         })
                         .OnComplete(() =>
                         {
                             InstImgSeed.DOScale(1, 0);
                             InstImgSeed.gameObject.SetActive(false);
                         });
        }


      
        public void OnClickBtnCoinShop()
        {
            InstPanelCoinShop.SetActive(true);
            //*터치 이미지 안보이게 해준다 */
            InstImgTouch.gameObject.SetActive(false);
        }


    }
}