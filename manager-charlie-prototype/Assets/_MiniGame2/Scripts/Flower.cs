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


        private Coroutine mFlowerLife;
        private Coroutine mRandomNutrients;

        /** @brief text로 정보를 보여주기 위해 사용 */
        private Coroutine mShowTextFlowerInfo;



        /** @brief  FertilizerScheduler, 1번 WaterScheduler */
        public List<NutrientScheduler> NutrientSchedulers = new List<NutrientScheduler>();

        //*현재 위치한 화분 번호*/
        public int PotNumber;

        
        /** @brief 꽃이 받은 물의 횟수 */
        public int AmountOfWater;
        /** @brief 꽃이 받은 비료의 횟수 */
        public int AmountOfFertilizer;


        //*물 비료 부족하다는 표시가 화면에 한 떴었는지 체크 :  true면 한번 화면에 나왔다는 뜻 */
        bool OnImgLackWater;
        bool OnImgLackFer;

        public int FlowerLevel;

        int mAmountOfWaterForLvUp;
        int mAmountOfFertilizerForLvUp;

        private bool mActiveWaterTimer;
        private bool mActiveFertilizerTimer;



        private void Start()
        {
            for (int i = 0; i < NutrientSchedulers.Count; i++)
            {
                NutrientSchedulers[i].SetFlower(this);
                NutrientSchedulers[i].PotNumber = PotNumber;
            }
        }

        public void SetScene(SceneMiniGame2 scene)
        {
            mScene = scene;
        }




        //* 화분에 새로 심어 질 때, 데이터 로드할 때 호출이 된다*/
        public void StartFlowerLife()
        {
            mFlowerLife = StartCoroutine(FlowerLife());
            mRandomNutrients = StartCoroutine(NutrientRandom());
            mShowTextFlowerInfo = StartCoroutine(ShowTextFlowerInfo());


            SetAmountOfNutrientForLv(FlowerLevel);

        }

        //* 죽을때*/
        public void StopFlowerLife()
        {
            StopCoroutine(mFlowerLife);
            StopCoroutine(mRandomNutrients);
            StopCoroutine(mShowTextFlowerInfo);
        }

        /**
         @fn    IEnumerator NutrientRandom()
        
         @brief 5초마다 랜덤으로 어떤 영양분 Scheduler를 활성화 할 지 선택한다
        
         @author    JT & YT
         @date  2017-09-05
        
         @return    An IEnumerator.
         */
        IEnumerator NutrientRandom()
        {
            while (true)
            {
                yield return new WaitForSeconds(5.0f);
                RandomNutrients();
            }
        }

        /**
         @fn    IEnumerator FlowerLife()
        
         @brief 1초마다 꽃의 영양분 Updata 
        
         @author    JT & YT
         @date  2017-09-05
        
         @return    An IEnumerator.
         */
        IEnumerator FlowerLife()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                UpdateNutrientsTimer();
               
            }
        }

        /**
         @fn    IEnumerator ShowTextFlowerInfo()
        
         @brief 꽃의 정보를 UI 에서 볼 수 있게 전달
        
         @author    JT & YT
         @date  2017-09-05
        
         @return    An IEnumerator.
         */
        IEnumerator ShowTextFlowerInfo()
        {
            while (true)
            {

                yield return new WaitForSeconds(0.2f);

                NutrientSchedulers[0].ShowTextNutrientInfo(AmountOfFertilizer, FlowerLevel);
                NutrientSchedulers[1].ShowTextNutrientInfo(AmountOfWater, FlowerLevel);

            }

        }

        void UpdateNutrientsTimer()
        {
            if (mActiveWaterTimer == true && mActiveFertilizerTimer == false)
            {
                NutrientSchedulers[1].UpdateTimer();
            }
            else if (mActiveWaterTimer == false && mActiveFertilizerTimer == true)
            {
                NutrientSchedulers[0].UpdateTimer();
            }
            else if (mActiveWaterTimer == true && mActiveFertilizerTimer == true)
            {
                NutrientSchedulers[0].UpdateTimer();
                NutrientSchedulers[1].UpdateTimer();
            }
        }


        //* timer를 활성화 할 영양분을 선택한다*/
        void RandomNutrients()
        {
            int randInt = Random.RandomRange(0,2);
            if (mActiveWaterTimer == true && mActiveFertilizerTimer == false)
            {
                if (randInt == 0)
                {
                    mActiveFertilizerTimer = true;
                }
            }
            else if (mActiveWaterTimer == false && mActiveFertilizerTimer == true)
            {
                if (randInt == 1)
                {
                    mActiveWaterTimer = true;
                }
            }
            else if (mActiveWaterTimer == false && mActiveFertilizerTimer == false)
            {
                if (randInt == 0)
                {
                    mActiveFertilizerTimer = true;
                }
                else
                {
                    mActiveWaterTimer = true;
                }
            }
        }

        /**
         @fn    public void DeadFlower()
        
         @brief NutrientScheduler에서 NutrientState가  Dead 상태일 때 호출 -> 화분의 정보를 초기화 한다*
        
         @author    JT & YT
         @date  2017-09-05
         */
        public void DeadFlower()
        {
            //* 데이터 지워줌*/
            mScene.ErasePotInfo(PotNumber);

            mActiveFertilizerTimer = false;
            mActiveWaterTimer = false;

            
            //* 코루딘 정지*/
            StopFlowerLife();

            //*임시: image 초기화 */
            transform.GetComponent<Image>().color = Color.white;
        }



        /**
         @fn    public void CheckLevelUp()
        
         @brief 물이나 비료를 줄 때마다 업그레이드 될 수 있는 상태인지 체크를 해준다
        
         @author    JT & YT
         @date  2017-09-05
         */
        public void CheckLevelUp()
        {
            if (AmountOfWater >= mAmountOfWaterForLvUp && AmountOfFertilizer >= mAmountOfFertilizerForLvUp)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            CDebug.Log("레벨 업 !");
            FlowerLevel = FlowerLevel + 1;

            AmountOfWater = 0;
            AmountOfFertilizer = 0;


            OnImgLackWater = false;

            
            //*결핍 상태 가기 까지의 시간을 레벨에 따라 설정해준다*/
            NutrientSchedulers[0].SetLackTime(FlowerLevel);
            NutrientSchedulers[1].SetLackTime(FlowerLevel);


            SetAmountOfNutrientForLv(FlowerLevel);
        }

        //*레벨에 따라 필요한 물, 비료를 설정해준다 -> 추후 변경 가능*/
        public void SetAmountOfNutrientForLv(int flowerLevel)
        {
            if (flowerLevel == 0) // 씨앗 단계
            {
                mAmountOfWaterForLvUp = 1;
                mAmountOfFertilizerForLvUp = 1;
            }
            else if (flowerLevel == 1) // 새싹 단계
            {
                mAmountOfWaterForLvUp = 1;
                mAmountOfFertilizerForLvUp = 1;

            }
            else if (flowerLevel == 2) // 줄기 단계
            {
                mAmountOfWaterForLvUp = 1;
                mAmountOfFertilizerForLvUp = 1;
            }
            else if (flowerLevel == 3) // 꽃봉오리
            {
                mAmountOfWaterForLvUp = 1;
                mAmountOfFertilizerForLvUp = 1;

            }
            else if (flowerLevel >= 4) // 꽃 = 만렙
            {
                mAmountOfWaterForLvUp = 5000000; //임시
                mAmountOfFertilizerForLvUp = 0;
            }
        }

        //* 물을 뿌려줄 때 첫번째로 호출 됨 */
        public void PlusWater()
        {
            AmountOfWater = AmountOfWater + 1;

            CheckLevelUp();

            //*물 받았으니 일반 상태 변경 */
            if (NutrientSchedulers[1].NowNutrientState == 1)
            {
                NutrientSchedulers[1].NormalNutrient();
            }

            mActiveWaterTimer = false;
        }

        //* 물을 뿌려줄 때 첫번째로 호출 됨 */
        public void PlusFertilizer()
        {
            AmountOfFertilizer = AmountOfFertilizer + 1;

            CheckLevelUp();

            //*영양분 받았으니 일반 상태 변경 */
            if (NutrientSchedulers[0].NowNutrientState == 1)
            {
                NutrientSchedulers[0].NormalNutrient();
            }

            mActiveFertilizerTimer = false;
        }


        //* 레벨업 할 때 호출*/
        public void InitLevelUpFlowerInfo()
        {
            //*진행된 데이터 초기화*/
            OnImgLackWater = false;
         //   flowerState = FlowerState.Normal;
            AmountOfWater = 0;
            AmountOfFertilizer = 0;
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

        /**
         @fn    public void SetFlowerData(bool isEmpty, int itemNumber)
        
         @brief 꽃을 심었을 때 정보를 초기화해서 설정해준다.
            isEmpty = false 상태는 꽃이 이미 있는상태, coroutine 호출하지 않는다
        
         @author    JT & YT
         @date  2017-09-05
        
         @param isEmpty     True if this object is empty.
         @param itemNumber  The item number.
         */
        public void SetFlowerData(bool isEmpty, int itemNumber)
        {
            mActiveWaterTimer = true;
            mActiveFertilizerTimer = false;


            AmountOfWater = 0;
            AmountOfFertilizer = 0;

            FlowerLevel = 0;

            SetFlowerColor(itemNumber);

            //*영양분 timer 초기화 */
            NutrientSchedulers[0].InitNutrient();
            NutrientSchedulers[1].InitNutrient();

            if (isEmpty == true)
            {
                StartFlowerLife();
            }
            else
            {

            }
        }
    }
}


