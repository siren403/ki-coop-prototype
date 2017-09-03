using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class MiniGame2PotClass
    {
        //* 화분 ID 값 */
        public int PotID;
        //* 비어있는지 체크*/
        public bool IsEmpty;


        //*꽃의 정보 */
        public int FlowerInfo;
        //* 물을 준 횟수*/
        public int WaterInfo;
        //* 비료 준 횟수*/
        public int FertilizerInfo;

        //* 물, 비료 타이머*/
        public int WaterTimer;
        public int FertilizerTimer;

        //*현재 꽃의 단계 */
        public int FlowerStep;


        public MiniGame2PotClass(int potId, bool isEmpty, int flowerInfo, int waterInfo, int fertilizerInfo, int waterTimer, int fertilizerTimer, int flowerStep)
        {
            PotID = potId;
            IsEmpty = isEmpty;
            FlowerInfo = flowerInfo;
            WaterInfo = waterInfo;
            FertilizerInfo = fertilizerInfo;
            WaterTimer = waterTimer;
            FertilizerTimer = fertilizerTimer;
            FlowerStep = flowerStep;
        }
    }
}

