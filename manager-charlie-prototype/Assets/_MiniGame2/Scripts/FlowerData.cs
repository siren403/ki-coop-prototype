using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;

namespace MiniGame2
{
    public class FlowerData : MonoBehaviour
    {
        private SceneMiniGame2 mScene = null;

        //*꽃 라이프 코루틴 */
        private Coroutine mFlowerLife;

        private Coroutine mRandomNutrients;

        //*0번 : FertilizerScheduler, 1번 WaterScheduler */
        public List<NutrientScheduler> NutrientSchedulers = new List<NutrientScheduler>();

        //*현재 위치한 화분 번호*/
        public int PotNumber;

        //* 현재 진행 된 물 상태*/
        public int AmountOfWater;
        //* 현재 진행 된 비료 상태*/
        public int AmountOfFertilizer;

        //*test 용 text ->자식오브젝트*/ 
        Text InstTextWaterInfo;
        Text InstTextWaterTimer;
        Text InstTextFlowerStep;
        Text InstTextFlowerLevel;





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

        //* 꽃이 만들어 질 때 SceneMiniGame2 에서 꽃의 진행 정보를 설정해준다*/
        public void SetFlowerState(int loadFlowerStep)
        {
            ////* 물 타이머가 3 이상이면 물 부족하다는 표시가 안나오게 끔 해준다 */
            //if (WaterTimer > 3)
            //{
            //    OnImgLackWater = true;
            //}
            //// * 비료 타이머가 5 이상이면 물 부족하다는 표시가 안나오게 끔 해준다 */ 
            //if (FertilizerTimer > 5)
            //{
            //    OnImgLackFer = true;
            //}
        }


        //* 화분에 새로 심어 질 때 호출이 된다*/
        public void StartFlowerLife()
        {
            mFlowerLife = StartCoroutine(FlowerLife());
            mRandomNutrients = StartCoroutine(NutrientRandom());
        }

        //* 죽거나 덮어 씌워 질 때 */
        public void StopFlowerLife()
        {
            StopCoroutine(mFlowerLife);
            StopCoroutine(mRandomNutrients);
        }


        IEnumerator NutrientRandom()
        {
            while (true)
            {
                yield return new WaitForSeconds(5.0f);
                RandomNutrients();
            }
        }


        IEnumerator FlowerLife()
        {
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                UpdateNutrientsTimer();
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
            CDebug.Log("Random :: --- > "+ randInt);
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

        //* 죽었을 때 호출 -> 화분의 정보를 초기화 한다*/
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

        //* 물이나 비료를 줄 때마다 업그레이드 될 수 있는 상태인지 체크를 해준다*/
        public void CheckLevelUp()
        {
            CDebug.Log("물 , 비료 상태를 보고 업그레이드 할 수 있는지 확인함");
            if (AmountOfWater >= mAmountOfWaterForLvUp && AmountOfFertilizer >= mAmountOfFertilizerForLvUp)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            CDebug.Log("레벨 업 !");
            AmountOfWater = 0;
            AmountOfFertilizer = 0;


            OnImgLackWater = false;

            FlowerLevel = FlowerLevel + 1;
            
            //*결핍 상태 가기 까지의 시간을 설정해준다*/
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
                mAmountOfWaterForLvUp = 2;
                mAmountOfFertilizerForLvUp = 2;

            }
            else if (flowerLevel == 2) // 줄기 단계
            {
                mAmountOfWaterForLvUp = 3;
                mAmountOfFertilizerForLvUp = 3;
            }
            else if (flowerLevel == 3) // 꽃봉오리
            {
                mAmountOfWaterForLvUp = 4;
                mAmountOfFertilizerForLvUp = 4;

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

        //*죽거나 새로 심었을 때 호출*/
        public void InitFlowerInfo()
        {
            //*진행된 데이터 초기화*/
            OnImgLackWater = false;
           // flowerState = FlowerState.Normal;
            AmountOfWater = 0;
            AmountOfFertilizer = 0;

            FlowerLevel = 0;
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

        public void SetFlowerData(int potNumber, bool activeWaterTimer, bool activeFertilizerTimer, int itemNumber)
        {
            PotNumber = potNumber;

            mActiveWaterTimer = activeWaterTimer;
            mActiveFertilizerTimer = activeFertilizerTimer;

            SetFlowerColor(itemNumber);

            //*영양분 timer 초기화 */
            NutrientSchedulers[0].InitNutrient();
            NutrientSchedulers[1].InitNutrient();
        }
    }
}


