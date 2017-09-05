using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiniGame2
{
    public class FlowerData
    {
        //* 비어있는지 체크*/
        public bool IsEmpty;

        //*꽃의 정보 (몇번 item 꽃이 들어갔는지) */
        public int FlowerNumber;

        //* 물을 준 횟수*/
        public int AmountOfWater;
        //* 비료 준 횟수*/
        public int AmountOfFertilizer;

        //* 물, 비료 타이머*/
        public int WaterTimer;  
        public int FertilizerTimer;

        //*물 , 비료 상태 */
        public int WatertState;
        public int FertilizerState;

        //*꽃 레벨*/
        public int FlowerLevel;

        public FlowerData(bool isEmpty,  int flowerNumber, int amountOfWater, int amountOfFertilizer, int waterTimer, int fertilizerTimer,int waterState , int fertilizerState, int flowerLevel)
        {
            IsEmpty = isEmpty;
            FlowerNumber = flowerNumber;

            AmountOfWater = amountOfWater;
            AmountOfFertilizer = amountOfFertilizer;

            WaterTimer = waterTimer;
            FertilizerTimer = fertilizerTimer;

            WatertState = waterState;
            FertilizerState = fertilizerState;

            FlowerLevel = flowerLevel;
        }
    }
}

