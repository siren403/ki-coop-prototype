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
        public int FlowerNumber;

        //* 물을 준 횟수*/
        public int AmountOfWater;
        //* 비료 준 횟수*/
        public int AmountOfFertilizer;

        //* 물, 비료 타이머*/
        public int WaterTimer;
        public int FertilizerTimer;

        //*꽃 상태 */
        public int FlowerState;

        //*꽃 레벨*/
        public int FlowerLevel;

        public MiniGame2PotClass(int potId, bool isEmpty, int flowerNumber, int amountOfWater, int amountOfFertilizer, int waterTimer, int fertilizerTimer, int flowerState, int flowerLevel)
        {
            PotID = potId;
            IsEmpty = isEmpty;
            FlowerNumber = flowerNumber;
            AmountOfWater = amountOfWater;
            AmountOfFertilizer = amountOfFertilizer;
            WaterTimer = waterTimer;
            FertilizerTimer = fertilizerTimer;
            FlowerState = flowerState;
            FlowerLevel = flowerLevel;
        }
    }
}

