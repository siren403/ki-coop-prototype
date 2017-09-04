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
        public List<FlowerData> Flower = null;

        public List<PotData> PotList = new List<PotData>();

        public List<GameObject> FlowerList = new List<GameObject>();

        //*단순히 화분 위치값을 가져오기 위해 사용됨*/
        public List<GameObject> InstBtnPots = new List<GameObject>();

        //*뒷 배경 색상 바꿔주기 (추후 화분 색상 바꿔주는것으로 변경) */
        public Image InstPanelGarden;

        void Awake()
        {
            UIShop.SetScene(this);
            UIMini2.SetScene(this);

            for (int i = 0; i < Flower.Count; i++)
            {
                Flower[i] = FlowerList[i].GetComponent<FlowerData>();
                Flower[i].SetScene(this);
            }
        }

        void Start()
        {
            LoadPotData();
            //InitPotDataList();
        }

        //* 화분 정보를 초기화 한다*/
        void InitPotDataList()
        {
            CDebug.Log("저장된 데이터 초기화");
            PotList.Add(new PotData(0, true, 0, 0, 0, 0, 0, 0, 0));
            PotList.Add(new PotData(1, true, 0, 0, 0, 0, 0, 0, 0));
            PotList.Add(new PotData(2, true, 0, 0, 0, 0, 0, 0, 0));
            PotList.Add(new PotData(3, true, 0, 0, 0, 0, 0, 0, 0));
        }

        void LoadPotData()
        {
            CDebug.Log("저장된 데이터 로드");
            string Jsonstring = File.ReadAllText(Application.dataPath + "/_MiniGame2/Resource/PotData.json");

            JsonData potData = JsonMapper.ToObject(Jsonstring);

            ParsingJsonItem(potData);
        }

        //* 저장된 데이터*/
        void ParsingJsonItem(JsonData potDataJson)
        {
            int potId;
            bool isEmpty;
            int flowerInfo;
            int waterInfo;
            int fertilizerInfo;
            int waterTimer;
            int fertilizerTimer;
            int flowerState;
            int flowerLevel;

            for (int i = 0; i < potDataJson.Count; i++)
            {
                potId = int.Parse(potDataJson[i]["PotID"].ToString());
                isEmpty = bool.Parse(potDataJson[i]["IsEmpty"].ToString());
                flowerInfo = int.Parse(potDataJson[i]["FlowerNumber"].ToString());
                waterInfo = int.Parse(potDataJson[i]["AmountOfWater"].ToString());
                fertilizerInfo = int.Parse(potDataJson[i]["AmountOfFertilizer"].ToString());
                waterTimer = int.Parse(potDataJson[i]["WaterTimer"].ToString());
                fertilizerTimer = int.Parse(potDataJson[i]["FertilizerTimer"].ToString());
                flowerState = int.Parse(potDataJson[i]["FlowerState"].ToString());
                flowerLevel = int.Parse(potDataJson[i]["FlowerLevel"].ToString());

                //*Json에 저장된 정보에 따라서 PotList에 들어감 */
                PotList.Add(new PotData(potId, isEmpty, flowerInfo, waterInfo, fertilizerInfo, waterTimer, fertilizerTimer, flowerState, flowerLevel));
            }

            SetFlowerList();
        }


        //* 저장된 화분 데이터에 따라서 꽃 이미지와 정보를 화면에 뿌려준다*/
        void SetFlowerList()
        {
            for (int i = 0; i < PotList.Count; i++)
            {
                if (PotList[i].IsEmpty == false)
                {
                    CDebug.Log(i + "번째 화분에" + PotList[i].FlowerNumber + " 번 식물 배치");

                    FlowerList[i].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;
                    FlowerList[i].GetComponent<FlowerData>().PotNumber = i;
                    FlowerList[i].GetComponent<FlowerData>().SetFlowerColor(PotList[i].FlowerNumber);
                    FlowerList[i].GetComponent<FlowerData>().AmountOfWater = PotList[i].AmountOfWater;
                    FlowerList[i].GetComponent<FlowerData>().WaterTimer = PotList[i].WaterTimer;
                    FlowerList[i].GetComponent<FlowerData>().FertilizerTimer = PotList[i].FertilizerTimer;
                    FlowerList[i].GetComponent<FlowerData>().SetFlowerState(PotList[i].FlowerState);
                    FlowerList[i].GetComponent<FlowerData>().FlowerLevel = PotList[i].FlowerLevel;
                }
            }
        }


        public void SaveJsonData()
        {
            CDebug.Log("데이터 저장 ! ");

            //* 꽃 정보 받아와서 저장해주는 함수*/
            SaveFlowerData();

            JsonData PotListJson = JsonMapper.ToJson(PotList);
            File.WriteAllText(Application.dataPath + "/_MiniGame2/Resource/PotData.json", PotListJson.ToString());
        }


        //* UIMiniGame2CoinShop 에서 호출됨 */
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

                //* 일단 직접적으로 바꿔줌*/
                if (itemNumber == 4)
                {
                    InstPanelGarden.color = Color.red;
                }
                else if (itemNumber == 5)
                {
                    InstPanelGarden.color = Color.white;
                }
                else if (itemNumber == 6)
                {
                    InstPanelGarden.color = Color.blue;
                }
                else if (itemNumber == 7)
                {
                    InstPanelGarden.color = Color.green;
                }
            }
        }



        //* 꽃 종류, 정보 등을 설정해준다*/
        void SetFlowerInfo(int itemNumber)
        {
            for (int i = 0; i < PotList.Count; i++)
            {
                if (PotList[i].IsEmpty == true)
                {
                    CDebug.Log(i + "번째 화분에" + itemNumber + " 번 아이템을 심는다");
                    PotList[i].IsEmpty = false;
                    PotList[i].FlowerNumber = itemNumber;

                    //*전에 있던 데이터 초기화*/
                    FlowerList[i].GetComponent<FlowerData>().InitFlowerInfo();

                    //*꽃 정보 설정*/
                    FlowerList[i].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + itemNumber;

                    FlowerList[i].GetComponent<FlowerData>().StartFlowerLife();
                    FlowerList[i].GetComponent<FlowerData>().PotNumber = i;
                    FlowerList[i].GetComponent<FlowerData>().SetFlowerColor(itemNumber);
                    FlowerList[i].GetComponent<FlowerData>().SetFlowerState(1);

                    //*꽃 심기는 UI 를 보여준다 */
                    UIMini2.ShowPlantFlower(itemNumber, i);
                    break;
                }
                //*화분이 다 찼을 때 첫 화분에 심기*/
                if (i == PotList.Count - 1)
                {
                    CDebug.Log("모든 화분이 다 찼다 -> 1번에 심는다.");
                    PotList[0].IsEmpty = false;
                    PotList[0].FlowerNumber = itemNumber;

                    //*전에 있던 데이터 초기화*/
                    FlowerList[0].GetComponent<FlowerData>().InitFlowerInfo();

                    //*꽃 정보 설정*/
                    FlowerList[0].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + itemNumber;

                    FlowerList[0].GetComponent<FlowerData>().StartFlowerLife();
                    FlowerList[0].GetComponent<FlowerData>().PotNumber = 0;
                    FlowerList[0].GetComponent<FlowerData>().SetFlowerColor(itemNumber);
                    FlowerList[0].GetComponent<FlowerData>().SetFlowerState(1);

                    //*꽃 심기는 UI 를 보여준다 */
                    UIMini2.ShowPlantFlower(itemNumber, i);

                }
            }
            SaveJsonData();
        }

        //*지운 꽃의 화분 정보 초기화한다*/
        public void ErasePotInfo(int potNumber)
        {
            PotList[potNumber].PotID = 0;
            PotList[potNumber].IsEmpty = true;
            PotList[potNumber].FlowerNumber = 0;
            PotList[potNumber].AmountOfWater = 0;
            PotList[potNumber].AmountOfFertilizer = 0;
            PotList[potNumber].WaterTimer = 0;
            PotList[potNumber].FertilizerTimer = 0;
            PotList[potNumber].FlowerNumber = 0;
            PotList[potNumber].FlowerLevel = 0;

            SaveJsonData();
        }

        //* 화분 눌렀을 때 호출*/
        public void OnClickBtnPot(int potNumber)
        {
            CDebug.Log("화분 정보 \n" + "PotList[potNumber].PotID : " + PotList[potNumber].PotID + "\n" + "PotList[potNumber].IsEmpty : " + PotList[potNumber].IsEmpty + "\n" +
                "PotList[potNumber].FlowerInfo : " + PotList[potNumber].FlowerNumber + "\n" + "PotList[potNumber].AmountOfWater : " + PotList[potNumber].AmountOfWater + "\n" +
                "PotList[potNumber].AmountOfFertilizer : " + PotList[potNumber].AmountOfFertilizer + "\n"
                );
        }

        //* 꽃에 물을 준다 */
        public void WaterTheFlower(int potNumber)
        {
            CDebug.Log(potNumber + "번 화분에 물뿌려준다 !");
            if (FlowerList[potNumber].GetComponent<FlowerData>().SavedFlowerState == 1)
            {
                CDebug.Log("물 줄 필요 없다 ! , 오반응 효과");
            }
            else
            {
                FlowerList[potNumber].GetComponent<FlowerData>().PlusWater();
            }
        }

        //*꽃 데이터를 저장한다 */
        void SaveFlowerData()
        {
            for (int i = 0; i < FlowerList.Count; i++)
            {
                PotList[i].AmountOfWater = FlowerList[i].GetComponent<FlowerData>().AmountOfWater;
                PotList[i].AmountOfFertilizer = FlowerList[i].GetComponent<FlowerData>().AmountOfWater;
                PotList[i].WaterTimer = FlowerList[i].GetComponent<FlowerData>().WaterTimer;
                PotList[i].FertilizerTimer = FlowerList[i].GetComponent<FlowerData>().FertilizerTimer;
                PotList[i].FlowerState = FlowerList[i].GetComponent<FlowerData>().SavedFlowerState;
                PotList[i].FlowerLevel = FlowerList[i].GetComponent<FlowerData>().FlowerLevel;
            }
        }
    }
}
