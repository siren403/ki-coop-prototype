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
        public List<Text> InstTextCurrentLevel = new List<Text>();

        public List<Image> InstImgLackWater = new List<Image>();
        public List<Text> InstTextWaterState = new List<Text>();
        public List<Text> InstTextCurrentWater = new List<Text>();
        public List<Text> InstTextWaterTimer = new List<Text>();

        public List<Image> InstImgLackFertilizer = new List<Image>();
        public List<Text> InstTextFerState = new List<Text>();
        public List<Text> InstTextCurrentFer = new List<Text>();
        public List<Text> InstTextFerTimer = new List<Text>();


        public List<Text> InstTextFlowerList = new List<Text>();


        private SceneMiniGame2 mScene = null;

        //*GameShop 화분 버튼 리스트 */
        public List<Button> InstBtnPotList = new List<Button>();

        //* 물 뿌리개, 비료 버튼 */
        public List<Button> InstBtnNutrientsList = new List<Button>();



        //*뒷 배경 색상 바꿔주기 (추후 화분 색상 바꿔주는것으로 변경) */
        public Image InstPanelGarden;



        public GameObject InstPanelTitle;

        public GameObject InstPanelCoinShop = null;

  

        Vector3 mBtnWaterPosition;

        Vector3 mBtnFertilizerPosition;

        public Button InstBtnExit;

        public Image InstImgTutorialTouch;

        public Transform InstImgSeed;

        int mSelectedNutrient;

        public float NutrientsMoveTime;


        public void Start()
        {
            NutrientsMoveTime = 0.3f;

            mSelectedNutrient = 3;
            InstImgTutorialTouch.DOFade(0, 1).SetLoops(-1, LoopType.Yoyo);
            InstBtnExit.onClick.AddListener(() => OnClickExit());

            InstBtnNutrientsList[0].onClick.AddListener(() => OnClickNutrients(0));
            InstBtnNutrientsList[1].onClick.AddListener(() => OnClickNutrients(1));


            mBtnWaterPosition = InstBtnNutrientsList[0].transform.position;
            mBtnFertilizerPosition = InstBtnNutrientsList[1].transform.position;


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


        //* 물뿌리개 선택하면 mSelectedNutrient = 1 , 비료면 0이됨*/
        public void OnClickNutrients(int nutrientId)
        {
            for (int i = 0; i < InstBtnNutrientsList.Count; i++)
            {
                if (i == nutrientId)
                {
                    InstBtnNutrientsList[i].transform.DOScale(Vector2.one * 1.5f, 0.5f).SetEase(Ease.OutBounce);
                    mSelectedNutrient = nutrientId;
                }
                else
                {
                    InstBtnNutrientsList[i].transform.DOKill();
                    InstBtnNutrientsList[i].transform.DOScale(Vector2.one , 0.5f).SetEase(Ease.OutBounce);
                }
            }           
            
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

        //*영양분이 움직이고 콜백으로 물주기 함수 호출 */
        public void OnClickBtnPot(int potNumber)
        {
            //* pot 번호 호출*/
            CDebug.Log(potNumber + "번 화분이 클릭 됨");


            //* 물주기 */
            if (mScene.FlowerData[potNumber].IsEmpty == true)
            {
                CDebug.Log("비어있다");
            }
            else
            {
                //* 물만 선택해서 주기*/
                if (mSelectedNutrient == 1)
                {
                    InstBtnNutrientsList[1].transform.DOMove(InstBtnPotList[potNumber].transform.position, NutrientsMoveTime).OnComplete(() =>
            InstWaterCallBack(potNumber));
                }

                //* 비료만 선택해서 주기*/
                else if (mSelectedNutrient == 0)
                {
                    InstBtnNutrientsList[0].transform.DOMove(InstBtnPotList[potNumber].transform.position, NutrientsMoveTime).OnComplete(() =>
         InstFertilizerCallBack(potNumber));

                }
                //* 필요한거 다 주기*/
                else
                {
                    InstBtnNutrientsList[0].transform.DOMove(InstBtnPotList[potNumber].transform.position, NutrientsMoveTime).OnComplete(() =>
         InstWaterCallBack(potNumber));

                    InstBtnNutrientsList[1].transform.DOMove(InstBtnPotList[potNumber].transform.position, NutrientsMoveTime).OnComplete(() =>
         InstFertilizerCallBack(potNumber));

                }
            }
        }

        void InstWaterCallBack(int potNumber)
        {
            CDebug.Log("물 뿌려주기 시작!");
            mScene.WaterTheFlower(potNumber);
            InstBtnNutrientsList[1].transform.DOMove(mBtnFertilizerPosition, 0);
            InstBtnNutrientsList[1].transform.DOScale(Vector2.one , 0);
            mSelectedNutrient = 2;
        }

        void InstFertilizerCallBack(int potNumber)
        {
            CDebug.Log("비료 뿌려주기 시작!");
            mScene.FertilizeTheFlower(potNumber);
            InstBtnNutrientsList[0].transform.DOMove(mBtnWaterPosition, 0);
            InstBtnNutrientsList[0].transform.DOScale(Vector2.one, 0);
            mSelectedNutrient = 2;
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
            InstImgTutorialTouch.gameObject.SetActive(false);
        }

        public void ShowTextWaterInfo(int nutrientState, int timer, int currentAmount, int level, int potNumber)
        {
            InstTextWaterTimer[potNumber].text = "w.timer: " + timer;
            InstTextCurrentWater[potNumber].text = "w.amount :" + currentAmount;
            if (nutrientState == 0)
            {
                InstTextWaterState[potNumber].text = "w. normal";
            }
            else
            {
                InstTextWaterState[potNumber].text = "w. lack";
            }

            InstTextCurrentLevel[potNumber].text = "lv : " + level ;            
        }


        public void ShowTextFertilizerInfo(int nutrientState, int timer, int currentAmount,  int level, int potNumber)
        {
            InstTextFerTimer[potNumber].text = "f.timer: " + timer;
            InstTextCurrentFer[potNumber].text = "f.amount :" + currentAmount;
            if (nutrientState == 0)
            {
                InstTextFerState[potNumber].text = "f. normal";
            }
            else
            {
                InstTextFerState[potNumber].text = "f. lack";
            }
            InstTextCurrentLevel[potNumber].text = "lv : " + level;
        }

        public void ActiveUILackFertilizer(int potNumber)
        {
            InstImgLackFertilizer[potNumber].DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
        }
        public void InActiveUILackFertilizer(int potNumber)
        {
            InstImgLackFertilizer[potNumber].DOKill();
            InstImgLackFertilizer[potNumber].DOFade(0, 0);
        }

        public void ActiveUILackWater(int potNumber)
        {
            InstImgLackWater[potNumber].DOFade(1, 1).SetLoops(-1, LoopType.Yoyo);
        }

        public void InActiveUILackWater(int potNumber)
        {
            InstImgLackWater[potNumber].DOKill();
            InstImgLackWater[potNumber].DOFade(0, 0);
        }


        public void InActiveUILackNutrients(int potNumber)
        {
            InstImgLackWater[potNumber].DOKill();
            InstImgLackWater[potNumber].DOFade(0, 0);

            InstImgLackFertilizer[potNumber].DOKill();
            InstImgLackFertilizer[potNumber].DOFade(0, 0);
        }

        public void SetPotColor(int itemNumber)
        {
            if (itemNumber == 4)
            {
                InstPanelGarden.color = Color.red;
            }
            else if (itemNumber == 5)
            {
                InstPanelGarden.color = Color.white;
            }
            else if (itemNumber == 6)
            {
                InstPanelGarden.color = Color.blue;
            }
            else if (itemNumber == 7)
            {
                InstPanelGarden.color = Color.green;
            }
        }

        /**
         @fn    public void SetFlowerText(int flowerNumber)
        
         @brief 꽃 이름을 정해주는 함수 -> 꽃 심을 때 호출
        
         @author    JT & YT
         @date  2017-09-05
        
         @param flowerNumber    The flower number.
         */
        public void SetFlowerText(int flowerNumber, int itemNumber)
        {
            InstTextFlowerList[flowerNumber].text = "Flower" + itemNumber;
        }
    }
}