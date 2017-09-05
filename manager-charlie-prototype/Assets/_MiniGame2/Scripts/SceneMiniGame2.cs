using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using CustomDebug;
using LitJson;
using Util;

namespace MiniGame2
{
    public class SceneMiniGame2 : MonoBehaviour
    {
        public UIMiniGame2CoinShop UIShop = null;
        public UIMiniGame2 UIMini2 = null;

        public List<Flower> Flower = null;
        public List<FlowerData> FlowerData = new List<FlowerData>();

        void Awake()
        {
            UIShop.SetScene(this);
            UIMini2.SetScene(this);

            for (int i = 0; i < Flower.Count; i++)
            {
                Flower[i].SetScene(this);
            }
        }

        void Start()
        {
            LoadPotData();
            //InitPotDataList();
        }

        //* 화분 정보를 초기화 */
        void InitPotDataList()
        {
            CDebug.Log("저장된 데이터 초기화");
            FlowerData.Add(new FlowerData(true, 0, 0, 0, 0, 0, 0, 0, 0));
            FlowerData.Add(new FlowerData(true, 0, 0, 0, 0, 0, 0, 0, 0));
            FlowerData.Add(new FlowerData(true, 0, 0, 0, 0, 0, 0, 0, 0));
            FlowerData.Add(new FlowerData(true, 0, 0, 0, 0, 0, 0, 0, 0));
        }

        void LoadPotData()
        {
            CDebug.Log("저장된 데이터 로드");
            string Jsonstring = File.ReadAllText(Application.dataPath + "/_MiniGame2/Resource/PotData.json");

            JsonData potData = JsonMapper.ToObject(Jsonstring);

            ParsingJsonItem(potData);
        }

        /**
         @fn    void ParsingJsonItem(JsonData potDataJson)
        
         @brief 저장된 데이터 Parsing
        
         @author    JT & YT
         @date  2017-09-05
        
         @param potDataJson The pot data JSON.
         */
        void ParsingJsonItem(JsonData potDataJson)
        {
            bool isEmpty;
            int flowerInfo;

            int amountOfWater;
            int amountOfFertilizerInfo;

            int waterTimer;
            int fertilizerTimer;

            int waterState;
            int fertilizerState;

            int flowerLevel;

            for (int i = 0; i < potDataJson.Count; i++)
            {
                isEmpty = bool.Parse(potDataJson[i]["IsEmpty"].ToString());
                flowerInfo = int.Parse(potDataJson[i]["FlowerNumber"].ToString());
                amountOfWater = int.Parse(potDataJson[i]["AmountOfWater"].ToString());
                amountOfFertilizerInfo = int.Parse(potDataJson[i]["AmountOfFertilizer"].ToString());
                waterTimer = int.Parse(potDataJson[i]["WaterTimer"].ToString());
                fertilizerTimer = int.Parse(potDataJson[i]["FertilizerTimer"].ToString());
                waterState = int.Parse(potDataJson[i]["WatertState"].ToString());
                fertilizerState = int.Parse(potDataJson[i]["FertilizerState"].ToString());
                flowerLevel = int.Parse(potDataJson[i]["FlowerLevel"].ToString());

                //*Json에 저장된 정보에 따라서 PotList에 들어감 */
                FlowerData.Add(new FlowerData(isEmpty, flowerInfo, amountOfWater, amountOfFertilizerInfo, waterTimer, fertilizerTimer, waterState, fertilizerState, flowerLevel));
            }

            LoadFlowerList();
        }

        /**
         @fn    void LoadFlowerList()
        
         @brief 저장된 화분 데이터에 로드
        
         @author    JT & YT
         @date  2017-09-05
         */
        void LoadFlowerList()
        {
            for (int i = 0; i < FlowerData.Count; i++)
            {
                if (FlowerData[i].IsEmpty == false)
                {
                    CDebug.Log(i + "번째 화분에" + FlowerData[i].FlowerNumber + " 번 식물 배치");

                    UIMini2.SetFlowerText(i, FlowerData[i].FlowerNumber);


                    Flower[i].SetFlowerColor(FlowerData[i].FlowerNumber);
                    Flower[i].AmountOfWater = FlowerData[i].AmountOfWater;
                    Flower[i].AmountOfFertilizer = FlowerData[i].AmountOfFertilizer;

                    Flower[i].NutrientSchedulers[0].Timer = FlowerData[i].FertilizerTimer;
                    Flower[i].NutrientSchedulers[1].Timer = FlowerData[i].WaterTimer;


                    Flower[i].NutrientSchedulers[0].NowNutrientState = FlowerData[i].FertilizerState;
                    Flower[i].NutrientSchedulers[1].NowNutrientState = FlowerData[i].WatertState;


                    Flower[i].FlowerLevel = FlowerData[i].FlowerLevel;
                    Flower[i].NutrientSchedulers[0].SetLackTime(Flower[i].FlowerLevel);
                    Flower[i].NutrientSchedulers[1].SetLackTime(Flower[i].FlowerLevel);
                    Flower[i].StartFlowerLife();
                }
            }
        }


        public void SaveJsonData()
        {
            //* 꽃 정보 받아와서 저장해주는 함수*/
            SaveFlowerData();

            JsonData PotListJson = JsonMapper.ToJson(FlowerData);
            File.WriteAllText(Application.dataPath + "/_MiniGame2/Resource/PotData.json", PotListJson.ToString());
        }

      
        void SaveFlowerData()
        {
            for (int i = 0; i < Flower.Count; i++)
            {
                FlowerData[i].AmountOfFertilizer = Flower[i].AmountOfWater;
                FlowerData[i].AmountOfWater = Flower[i].AmountOfWater;

                FlowerData[i].FertilizerTimer = Flower[i].NutrientSchedulers[0].Timer;
                FlowerData[i].WaterTimer = Flower[i].NutrientSchedulers[1].Timer;

                FlowerData[i].FertilizerState = Flower[i].NutrientSchedulers[0].NowNutrientState;
                FlowerData[i].WatertState = Flower[i].NutrientSchedulers[1].NowNutrientState;

                FlowerData[i].FlowerLevel = Flower[i].FlowerLevel;
            }
        }


        /**
         @fn    public void BuyItem(int itemNumber)
        
         @brief UIMiniGame2CoinShop 에서 호출

        @author JT & YT
         @date  2017-09-05
        
         @param itemNumber  The item number.
         */
        public void BuyItem(int itemNumber)
        {
            //* itemNumber  0~3 까지 꽃 아이템, 4 ~ 7 까지 화분 색상 변경 아이템*/
            CDebug.Log(itemNumber + " 번 아이템을 구매하였다 ! ");
            if (itemNumber <= 3)
            {
                CDebug.Log(itemNumber + " 번 꽃을 심는다 ! ");
                SetFlowerInfo(itemNumber);
            }
            else
            {
                CDebug.Log("화분 색상 바뀜 itemNumber : " + itemNumber);
                UIMini2.SetPotColor(itemNumber);
            }
        }

        /**
         @fn    void SetFlowerInfo(int itemNumber)
        
         @brief 구매한 꽃의 종류, 정보 등을 설정해준다

        @author JT & YT
         @date  2017-09-05
        
         @param itemNumber  The item number.
         */
        void SetFlowerInfo(int itemNumber)
        {
            for (int i = 0; i < FlowerData.Count; i++)
            {
                if (FlowerData[i].IsEmpty == true)
                {
                    CDebug.Log(i + "번째 화분에" + itemNumber + " 번 아이템을 심는다");

                    //*꽃의 정보를 설정해준다 */
                    Flower[i].SetFlowerData(FlowerData[i].IsEmpty, itemNumber);

                    FlowerData[i].IsEmpty = false;
                    FlowerData[i].FlowerNumber = itemNumber;

                    //*꽃 심기는 UI 를 보여준다 */
                    UIMini2.ShowPlantFlower(itemNumber, i);
                    //*꽃 정보 설정*/
                    UIMini2.SetFlowerText(i,itemNumber);

                    break;
                }
                //*화분이 다 찼을 때 첫 화분에 심기*/
                if (i == FlowerData.Count - 1)
                {

                    Flower[0].SetFlowerData(FlowerData[0].IsEmpty, itemNumber);

                    FlowerData[0].IsEmpty = false;
                    FlowerData[0].FlowerNumber = itemNumber;

                    //*꽃 심기는 UI 를 보여준다 */
                    UIMini2.ShowPlantFlower(itemNumber, 0);
                    //*꽃 정보 설정*/
                    UIMini2.SetFlowerText(i,itemNumber);

                }
            }
        }

        //*지운 꽃의 화분 정보 초기화한다*/
        public void ErasePotInfo(int potNumber)
        {
            FlowerData[potNumber].IsEmpty = true;
            FlowerData[potNumber].FlowerNumber = 0;

            FlowerData[potNumber].AmountOfWater = 0;
            FlowerData[potNumber].AmountOfFertilizer = 0;
            FlowerData[potNumber].WaterTimer = 0;
            FlowerData[potNumber].FertilizerTimer = 0;

            FlowerData[potNumber].WatertState = 0;
            FlowerData[potNumber].FertilizerState = 0;

            FlowerData[potNumber].FlowerLevel = 0;
        }




        public void WaterTheFlower(int potNumber)
        {
            if (Flower[potNumber].NutrientSchedulers[1].NowNutrientState == 0)
            {
                CDebug.Log("물 줄 필요 없다 ! , 오반응 효과");
            }
            else
            {
                CDebug.Log(potNumber + "번 화분에 물 뿌려준다 !");
                Flower[potNumber].GetComponent<Flower>().PlusWater();
            }
        }


        public void FertilizeTheFlower(int potNumber)
        {
            if (Flower[potNumber].NutrientSchedulers[0].NowNutrientState == 0)
            {
                CDebug.Log("비료 줄 필요 없다 ! , 오반응 효과");
            }
            else
            {
                CDebug.Log(potNumber + "번 화분에 비료 뿌려준다 !");
                Flower[potNumber].PlusFertilizer();
            }
        }

    }
}
