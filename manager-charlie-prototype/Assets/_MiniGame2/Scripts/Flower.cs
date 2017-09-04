using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;

namespace MiniGame2
{
    public class Flower : MonoBehaviour
    {
        private SceneMiniGame2 mScene = null;


        //*꽃의 정보를 담은 스크립트 */

        private Coroutine mFlowerLife;

        //*현재 위치한 화분 번호*/
        public int PotNumber;

        //* 현재 진행 된 물 상태*/
        public int AmountOfWater;
        //* 현재 진행 된 비료 상태*/
        public int AmountOfFertilizer;

        public int WaterTimer;
        public int FertilizerTimer;

        //* 물 부족하다고 알려주는 시간*/
        public int CountLackWater;
        public int CountlackFertilizerTimer;

        //*물 부족할때 죽는 시간*/
        int DeadTime;


        //*test 용 text ->자식오브젝트*/ 
        Text InstTextWaterInfo;
        Text InstTextWaterTimer;
        Text InstTextFlowerStep;
        Text InstTextFlowerLevel;


        public Image InstImgLackWater;
        public Image InstImgLackFer;


        //*물 비료 부족하다는 표시가 화면에 한 떴었는지 체크 :  true면 한번 화면에 나왔다는 뜻 */
        bool OnImgLackWater;
        bool OnImgLackFer;

        public int FlowerLevel;

        int mAmountOfWaterForLvUp;
        int mAmountOfFertilizerForLvUp;

        //*꽃의 단계 설정 */
        enum FlowerState
        {
            None =0,//아무것도 없을 때
            Normal , // 보통 상태
            LackWater, // 물 부족 상태
            LackFer, // 비료 부족 상태
            Dead, //죽음
        }

        FlowerState flowerState;

        public int SavedFlowerState
        {
            get
            {
                return (int)flowerState;
            }
        }

        private void Start()
        {
            //*꽃 죽는시간 초기화 */
            DeadTime = 10;

            //*코루틴  */
            mFlowerLife = StartCoroutine(FlowerLife());

            InstTextWaterInfo = transform.FindChild("InstTextWaterInfo").GetComponent<Text>();
            InstTextWaterTimer = transform.FindChild("InstTextWaterTimer").GetComponent<Text>();
            InstTextFlowerStep = transform.FindChild("InstTextFlowerStep").GetComponent<Text>();
            InstTextFlowerLevel = transform.FindChild("InstTextFlowerLevel").GetComponent<Text>();
           
            InstImgLackWater.gameObject.SetActive(false);
            InstImgLackFer.gameObject.SetActive(false);
        }
        public void SetScene(SceneMiniGame2 scene)
        {
            mScene = scene;
        }

        //* 꽃이 만들어 질 때 SceneMiniGame2 에서 꽃의 진행 정보를 설정해준다*/
        public void SetFlowerState(int loadFlowerStep)
        {
            //* 물 타이머가 3 이상이면 물 부족하다는 표시가 안나오게 끔 해준다 */
            if (WaterTimer > 3)
            {
                OnImgLackWater = true;
            }
            // * 비료 타이머가 5 이상이면 물 부족하다는 표시가 안나오게 끔 해준다 */ 
            if (FertilizerTimer > 5)
            {
                OnImgLackFer = true;
            }
            flowerState = (FlowerState)loadFlowerStep;
        }


        //* 화분에 새로 심어 질 때 호출이 된다*/
        public void StartFlowerLife()
        {
            //*이전 코루틴 멈추고 다시 시작 */
            StopCoroutine(mFlowerLife);
            mFlowerLife = StartCoroutine(FlowerLife());
        }


        IEnumerator FlowerLife()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);

                switch (flowerState)
                {
                    //*아무것도 심겨지지 않음*/
                    case FlowerState.None:

                    break;

                    //*일반 상태*/
                    case FlowerState.Normal:
                        WaterTimer = WaterTimer + 1;
          
                        if (WaterTimer > CountLackWater)
                        {
                            WaterTimer = 0;
                            flowerState = FlowerState.LackWater;
                        }
                        break;

                    //* 물 부족 상태*/
                    case FlowerState.LackWater:

                        WaterTimer = WaterTimer + 1;
                        LackWater();
                        if (WaterTimer > DeadTime)
                        {
                            WaterTimer = 0;
                            flowerState = FlowerState.Dead;
                        }
                        break;

                    //* 비료 부족 상태*/
                    case FlowerState.LackFer:

                        FertilizerTimer = FertilizerTimer + 1;
                        LackFertilizer();
                        if (FertilizerTimer > DeadTime)
                        {
                            FertilizerTimer = 0;
                            flowerState = FlowerState.Dead;
                        }
                        break;

                    case FlowerState.Dead:
                        CDebug.Log("죽음");
                        DeadFlower();
                        InitFlowerInfo();
                        flowerState = FlowerState.None;
                        break;
                }


                SetTestText();
            }
        }

        //*테스트를 위한 정보를 받기위해 text 사용  */
        void SetTestText()
        {
            InstTextWaterInfo.text = "W Info : " + AmountOfWater;
            InstTextWaterTimer.text = "W Timer : " + WaterTimer;
            InstTextFlowerStep.text = "" + flowerState;
            InstTextFlowerLevel.text = "Lv : " + FlowerLevel;
        }
        void CheckDeadTime()
        {
            //*내가 설정해준 DeadTime 보다 물,비료 주는 시간이 넘어가면 죽음 상태로 변경 */
            if (WaterTimer > DeadTime || FertilizerTimer > DeadTime)
            {
                flowerState = FlowerState.Dead;
            }
        }

        //* 물 부족할때 표시해줌*/
        void LackWater()
        {
            if (OnImgLackWater == false)
            {
                CDebug.Log(PotNumber + " 번 꽃 물이 부족합니다 -> 물 부족 이미지 띄워주기");
                InstImgLackWater.GetComponent<Image>().DOFade(1, 1).SetLoops(2, LoopType.Yoyo)
                         .OnStart(() =>
                         {
                             InstImgLackWater.gameObject.SetActive(true);
                             OnImgLackWater = true;
                         })
                         .OnComplete(() =>
                         {
                             InstImgLackWater.gameObject.SetActive(false);
                         });
            }
        }

        //* 비료 부족할때 표시해줌*/
        void LackFertilizer()
        {
            if (OnImgLackFer == false)
            {
                CDebug.Log(PotNumber + " 번 꽃 비료가 부족합니다 -> 비료 부족 이미지 띄워주기");
                InstImgLackFer.GetComponent<Image>().DOFade(1, 1).SetLoops(2, LoopType.Yoyo)
                         .OnStart(() =>
                         {
                             InstImgLackFer.gameObject.SetActive(true);
                             OnImgLackFer = true;
                         })
                         .OnComplete(() =>
                         {
                             InstImgLackFer.gameObject.SetActive(false);
                         });
            }
        }

        //* 죽었을 때 호출 -> 화분의 정보를 초기화 한다*/
        void DeadFlower()
        {
            mScene.ErasePotInfo(PotNumber);

            //*임시 Text , image 초기화 */
            transform.FindChild("Text").GetComponent<Text>().text = "None";
            transform.GetComponent<Image>().color = Color.white;
        }

        //* 물이나 비료를 줄 때마다 업그레이드 될 수 있는 상태인지 체크를 해준다*/
        public void CheckLevelUp()
        {
            CDebug.Log("물 , 비료 상태를 보고 업그레이드 할 수 있는지 확인함");
            if (AmountOfWater >= mAmountOfWaterForLvUp && AmountOfFertilizer >= mAmountOfFertilizerForLvUp )
            {
                    LevelUp();   
            }
        }

        public void LevelUp()
        {
            CDebug.Log("레벨 업 !");
            AmountOfWater = 0;
            AmountOfFertilizer = 0;
            WaterTimer = 0;
            FertilizerTimer = 0;

            OnImgLackWater = false;

            FlowerLevel = FlowerLevel + 1;
            SetAmountOfNutrientForLv(FlowerLevel);
        }

        //*레벨에 따라 필요한 물, 비료를 설정해준다 -> 추후 변경 가능*/
        public void SetAmountOfNutrientForLv(int flowerLevel)
        {
            if (flowerLevel == 0) // 씨앗 단계
            {
                mAmountOfWaterForLvUp = 1;
                mAmountOfFertilizerForLvUp = 0;
                CountLackWater = 6;
                CountlackFertilizerTimer = 6;
            }
            else if (flowerLevel == 1) // 새싹 단계
            {
                mAmountOfWaterForLvUp = 2;
                mAmountOfFertilizerForLvUp = 0;
                CountLackWater = 8;
                CountlackFertilizerTimer = 8;
            }
            else if (flowerLevel == 2) // 줄기 단계
            {
                mAmountOfWaterForLvUp = 3;
                mAmountOfFertilizerForLvUp = 0;

                CountLackWater = 10;
                CountlackFertilizerTimer = 10;
            }
            else if (flowerLevel == 3) // 꽃봉오리
            {
                mAmountOfWaterForLvUp = 4;
                mAmountOfFertilizerForLvUp = 0;

                CountLackWater = 12;
                CountlackFertilizerTimer = 12;
            }
            else if (flowerLevel == 4) // 꽃 = 만렙
            {
                mAmountOfWaterForLvUp = 5000000;
                mAmountOfFertilizerForLvUp = 0;
            }
        }

        //* 물을 뿌려줄 때 첫번째로 호출 됨 */
        public void PlusWater()
        {
            AmountOfWater = AmountOfWater + 1;
            WaterTimer = 0;
            CheckLevelUp();

            //*물 받았으니 꽃 상태 변경 */
            if (flowerState == FlowerState.LackWater)
            {
                flowerState = FlowerState.Normal;
            }
        }

        //* 물을 뿌려줄 때 첫번째로 호출 됨 */
        public void PlusFertilizer()
        {
            AmountOfFertilizer = AmountOfFertilizer + 1;
            FertilizerTimer = 0;
            CheckLevelUp();

            //*물 받았으니 꽃 상태 변경 */
            if (flowerState == FlowerState.LackFer)
            {
                flowerState = FlowerState.Normal;
            }
        }

        //*죽거나 새로 심었을 때 호출*/
        public void InitFlowerInfo()
        {
            //*진행된 데이터 초기화*/
            OnImgLackWater = false;
            flowerState = FlowerState.Normal;
            AmountOfWater = 0;
            AmountOfFertilizer = 0;
            WaterTimer = 0;
            FertilizerTimer = 0;
            FlowerLevel = 0;
        }

        //* 레벨업 할 때 호출*/
        public void InitLevelUpFlowerInfo()
        {
            //*진행된 데이터 초기화*/
            OnImgLackWater = false;
            flowerState = FlowerState.Normal;
            AmountOfWater = 0;
            AmountOfFertilizer = 0;
            WaterTimer = 0;
            FertilizerTimer = 0;
        }


        //* 꽃 색상 설정해 주는 함수*/
        public void SetFlowerColor(int itemNumber)
        {
            if (itemNumber == 0)
            {
                transform.GetComponent<Image>().color = Color.red;
            }
            else if (itemNumber == 1)
            {
                transform.GetComponent<Image>().color = Color.blue;
            }
            else if (itemNumber == 2)
            {
                transform.GetComponent<Image>().color = Color.yellow;
            }
            else if (itemNumber == 3)
            {
                transform.GetComponent<Image>().color = Color.green;
            }
        }
    }
}


