using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class NutrientScheduler : MonoBehaviour
    {
        /** @brief 시간을 재는 변수 */
        private int mTimer = 0;

        /** @brief 영양분이 부족하다고 느끼기 까지의 시간 */
        private int mLackNutrientTime = 3;


        /** @brief 비료를 주지 않았을 때까지의 죽는 시간 */
        private int mDeadTime = 10;


        //*FlowerData start에서 설정해줌 */
        public int PotNumber;

        //*꽃의 단계 설정 */
        enum NutrientState
        {
            Normal = 0, // 보통 상태
            Lack, //  부족 상태
            Dead, //죽음
        }

        NutrientState nutrientState;

        public int NowNutrientState
        {
            get
            {
                return (int)nutrientState;
            }
        }

        public int Timer
        {
            get
            {
                return mTimer;
            }
        }

        /** @brief Flower 스크립트 가져오기 */
        protected FlowerData mFlower;

        public void SetFlower(FlowerData flower)
        {
            mFlower = flower;
        }



        /**
         @fn    public void UpdateTimer()
        
         @brief Flower 에서 호출 됨 -> 현재 영양상태에 따라서 상태 변화 
        
         @author    JT & YT
         @date  2017-09-04
         */
        public void UpdateTimer()
        {
            mTimer = mTimer + 1;
            switch (nutrientState)
            {
                case NutrientState.Normal:
                    if (mTimer >= mLackNutrientTime)
                    {
                        LackNutrient();
                        mTimer = 0;
                        nutrientState = NutrientState.Lack;
                    }
                    break;

                case NutrientState.Lack:
                    if (mTimer >= mDeadTime)
                    {
                        mTimer = 0;
                        nutrientState = NutrientState.Dead;
                    }
                    break;

                case NutrientState.Dead:
                    DeadFlower();

                    break;
            }
        }



        /**
         @fn    public void SetSchedulerData(int timer)
        
         @brief 이전에 저장된 데이터가 있을때 데이터 로드에 사용
        
         @author    JT & YT
         @date  2017-09-04
        
         @param timer   The timer.
         */
        public void SetSchedulerData(int timer, int state)
        {
            mTimer = timer;
            nutrientState = (NutrientState)state;
        }

        public void InitNutrient()
        {
            mTimer = 0;
            mLackNutrientTime = 3;
            nutrientState = NutrientState.Normal;
        }
        public virtual void NormalNutrient()
        {
            mTimer = 0;
            nutrientState = NutrientState.Normal;
        }


        public virtual void LackNutrient()
        {

        }


        //*Text 형식으로 보여주기 위한 함수 */
        public virtual void ShowTextNutrientInfo(int currentAmount, int level)
        {


        }


        /**
 @fn    void DeadFlower()

 @brief 영양분이 부족해서 죽었을 때 호출 : FlowerData -> DeadFlower 함수 호출

 @author    JT & YT
 @date  2017-09-04
 */
        public virtual void DeadFlower()
        {
            mTimer = 0;
            nutrientState = NutrientState.Normal;
            mFlower.DeadFlower();
        }

        public void SetLackTime(int level)
        {
            if (level == 0)
            {
                mLackNutrientTime = 3;
            }
            else if (level == 1)
            {
                mLackNutrientTime = 5;
            }
            else if (level == 2)
            {
                mLackNutrientTime = 8;
            }
            else if (level == 3)
            {
                mLackNutrientTime = 15;
            }
        }

    }
}