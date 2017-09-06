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

            JsonData flowerJsonData = JsonMapper.ToObject(Jsonstring);

            ParsingJsonItem(flowerJsonData);
        }

        /**
         @fn    void ParsingJsonItem(JsonData potDataJson)
        
         @brief 저장된 데이터 Parsing
        
         @author    JT & YT
         @date  2017-09-05
        
         @param potDataJson The pot data JSON.
         */
        void ParsingJsonItem(JsonData flowerJsonData)
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

            for (int i = 0; i < flowerJsonData.Count; i++)
            {
                isEmpty = bool.Parse(flowerJsonData[i]["IsEmpty"].ToString());
                flowerInfo = int.Parse(flowerJsonData[i]["FlowerNumber"].ToString());
                amountOfWater = int.Parse(flowerJsonData[i]["AmountOfWater"].ToString());
                amountOfFertilizerInfo = int.Parse(flowerJsonData[i]["AmountOfFertilizer"].ToString());
                waterTimer = int.Parse(flowerJsonData[i]["WaterTimer"].ToString());
                fertilizerTimer = int.Parse(flowerJsonData[i]["FertilizerTimer"].ToString());
                waterState = int.Parse(flowerJsonData[i]["WatertState"].ToString());
                fertilizerState = int.Parse(flowerJsonData[i]["FertilizerState"].ToString());
                flowerLevel = int.Parse(flowerJsonData[i]["FlowerLevel"].ToString());

                //*Json에 저장된 정보에 따라서 FlowerData 설정 */
                FlowerData.Add(new FlowerData(isEmpty, flowerInfo, amountOfWater, amountOfFertilizerInfo, waterTimer, fertilizerTimer, waterState, fertilizerState, flowerLevel));
            }

            LoadFlowerList();
        }

        /**
         @fn    void LoadFlowerList()
        
         @brief 저장된 Data를 Flowr에 로드해줌
        
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


                    Flower[i].LoadFlower(FlowerData[i].FlowerNumber, FlowerData[i].AmountOfWater, FlowerData[i].AmountOfFertilizer
                     ,FlowerData[i].WaterTimer, FlowerData[i].FertilizerTimer, FlowerData[i].WatertState, FlowerData[i].FertilizerState , FlowerData[i].FlowerLevel);
                
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
                FlowerData[i].SaveFlowerData(Flower[i].AmountOfWater, Flower[i].AmountOfWater, Flower[i].NutrientSchedulers[1].Timer, Flower[i].NutrientSchedulers[0].Timer
                    , Flower[i].NutrientSchedulers[1].NowNutrientState, Flower[i].NutrientSchedulers[0].NowNutrientState, Flower[i].FlowerLevel);
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
                PlantFlower(itemNumber);
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
        void PlantFlower(int itemNumber)
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
                    CDebug.Log("itemNumber :: :" + itemNumber);
                    Flower[0].SetFlowerData(FlowerData[0].IsEmpty, itemNumber);

                    FlowerData[0].IsEmpty = false;
                    FlowerData[0].FlowerNumber = itemNumber;
     
                    //*꽃 심기는 UI 를 보여준다 */
                    UIMini2.ShowPlantFlower(itemNumber, 0);
                    //*꽃 정보 설정*/
                    UIMini2.SetFlowerText(0,itemNumber);
                }
            }
        }

        //*지운 꽃의 화분 정보 초기화한다*/
        public void EraseFlowerData(int potNumber)
        {
            FlowerData[potNumber].InitFlowerData();
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
