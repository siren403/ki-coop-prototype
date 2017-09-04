using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class NutrientScheduler
    {
        /** @brief 시간을 재는 변수 */
        private int mTimer = 0;

        /** @brief 영양분이 부족하다고 느끼기 까지의 시간 */
        private int mLackNutrientTime = 3;


        /** @brief 비료를 주지 않았을 때까지의 죽는 시간 */
        private int mDeadTime = 10;


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
                        mTimer = 0;
                        LackNutrient();
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
        
         @brief 저장된 데이터를 로드 할 때 호출
        
         @author    JT & YT
         @date  2017-09-04
        
         @param timer   The timer.
         */
        public void SetSchedulerData(int timer)
        {
            mTimer = timer;
        }



        /**
         @fn    void DeadFlower()
        
         @brief 영양분이 부족해서 죽었을 때 호출 : FlowerData -> DeadFlower 함수 호출
        
         @author    JT & YT
         @date  2017-09-04
         */
        void DeadFlower()
        {
            mTimer = 0;
            nutrientState = NutrientState.Normal;
            mFlower.DeadFlower();
        }

        public void SetStateNormal()
        {
            nutrientState = NutrientState.Normal;
        }
        public virtual void LackNutrient()
        {

        }
    }
}