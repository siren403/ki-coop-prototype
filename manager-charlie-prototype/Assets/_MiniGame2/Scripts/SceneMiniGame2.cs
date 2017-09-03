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

        public static SceneMiniGame2 instance = null;
        void Awake()
        {
            if (instance == null)
                instance = this;

            else if (instance != this)
                Destroy(gameObject);
        }


        public List<MiniGame2PotClass> PotList = new List<MiniGame2PotClass>();

        public List<GameObject> FlowerList = new List<GameObject>();

        //*단순히 화분 위치값을 가져오기 위해 사용됨*/
        public List<GameObject> InstBtnPots = new List<GameObject>();

        //*뒷 배경 색상 바꿔주기 (추후 화분 색상 바꿔주는것으로 변경) */
        public Image InstPanelGarden;

        void Start()
        {
            LoadPotData();
            //InitPotDataList();
        }

        //* 화분 정보를 초기화 한다*/
        void InitPotDataList()
        {
            CDebug.Log("저장된 데이터 초기화");
            PotList.Add(new MiniGame2PotClass(0, true, 0, 0, 0, 0, 0, 0));
            PotList.Add(new MiniGame2PotClass(1, true, 0, 0, 0, 0, 0, 0));
            PotList.Add(new MiniGame2PotClass(2, true, 0, 0, 0, 0, 0, 0));
            PotList.Add(new MiniGame2PotClass(3, true, 0, 0, 0, 0, 0, 0));
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
            int flowerStep;

            for (int i = 0; i < potDataJson.Count; i++)
            {
                potId = int.Parse(potDataJson[i]["PotID"].ToString());
                isEmpty = bool.Parse(potDataJson[i]["IsEmpty"].ToString());
                flowerInfo = int.Parse(potDataJson[i]["FlowerInfo"].ToString());
                waterInfo = int.Parse(potDataJson[i]["WaterInfo"].ToString());
                fertilizerInfo = int.Parse(potDataJson[i]["FertilizerInfo"].ToString());
                waterTimer = int.Parse(potDataJson[i]["WaterTimer"].ToString());
                fertilizerTimer = int.Parse(potDataJson[i]["FertilizerTimer"].ToString());
                flowerStep = int.Parse(potDataJson[i]["FlowerStep"].ToString());

                //*Json에 저장된 정보에 따라서 PotList에 들어감 */
                PotList.Add(new MiniGame2PotClass(potId, isEmpty, flowerInfo, waterInfo, fertilizerInfo, waterTimer, fertilizerTimer, flowerStep));
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
                    CDebug.Log(i + "번째 화분에" + PotList[i].FlowerInfo + " 번 식물 배치");

                    FlowerList[i].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().PotNumber = i;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SetFlowerColor(PotList[i].FlowerInfo);
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().WaterInfo = PotList[i].WaterInfo;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().WaterTimer = PotList[i].WaterTimer;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().FertilizerTimer = PotList[i].FertilizerTimer;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SetFlowerStep(PotList[i].FlowerStep);
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
                    PotList[i].FlowerInfo = itemNumber;

                    //*전에 있던 데이터 초기화*/
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().InitFlowerInfo();

                    //*꽃 정보 설정*/
                    FlowerList[i].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;

                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().StartFlowerLife();
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().PotNumber = i;
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SetFlowerColor(itemNumber);
                    FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SetFlowerStep(1);
                    break;
                }
                //*화분이 다 찼을 때 첫 화분에 심기*/
                if (i == PotList.Count - 1)
                {
                    CDebug.Log("모든 화분이 다 찼다 -> 1번에 심는다.");
                    PotList[0].IsEmpty = false;
                    PotList[0].FlowerInfo = itemNumber;

                    //*전에 있던 데이터 초기화*/
                    FlowerList[0].GetComponent<MiniGame2FlowerInfo>().InitFlowerInfo();

                    //*꽃 정보 설정*/
                    FlowerList[0].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;

                    FlowerList[0].GetComponent<MiniGame2FlowerInfo>().StartFlowerLife();
                    FlowerList[0].GetComponent<MiniGame2FlowerInfo>().PotNumber = 0;
                    FlowerList[0].GetComponent<MiniGame2FlowerInfo>().SetFlowerColor(itemNumber);
                    FlowerList[0].GetComponent<MiniGame2FlowerInfo>().SetFlowerStep(1);
                }
            }
            SaveJsonData();
        }

        //*지운 꽃의 화분 정보 초기화한다*/
        public void ErasePotInfo(int potNumber)
        {
            PotList[potNumber].PotID = 0;
            PotList[potNumber].IsEmpty = true;
            PotList[potNumber].FlowerInfo = 0;
            PotList[potNumber].WaterInfo = 0;
            PotList[potNumber].FertilizerInfo = 0;
            PotList[potNumber].WaterTimer = 0;
            PotList[potNumber].FertilizerTimer = 0;
            PotList[potNumber].FlowerStep = 0;

            SaveJsonData();
        }

        //* 화분 눌렀을 때 호출*/
        public void OnClickBtnPot(int potNumber)
        {
            CDebug.Log("화분 정보 \n" + "PotList[potNumber].PotID : " + PotList[potNumber].PotID + "\n" + "PotList[potNumber].IsEmpty : " + PotList[potNumber].IsEmpty + "\n" +
                "PotList[potNumber].FlowerInfo : " + PotList[potNumber].FlowerInfo + "\n" + "PotList[potNumber].WaterInfo : " + PotList[potNumber].WaterInfo + "\n" +
                "PotList[potNumber].FertilizerInfo : " + PotList[potNumber].FertilizerInfo + "\n"
                );
        }

        public void DeadFlower(int potNumber)
        {
            //*꽃 죽으면 그 화분의 정보를 초기화 */
            ErasePotInfo(potNumber);

            //*임시 Text , image 초기화 */
            FlowerList[potNumber].transform.FindChild("Text").GetComponent<Text>().text = "None";
            FlowerList[potNumber].GetComponent<Image>().color = Color.white;
        }

        //* 꽃에 물을 준다 */
        public void WaterTheFlower(int potNumber)
        {
            CDebug.Log(potNumber + "번 화분에 물뿌려준다 !");
            FlowerList[potNumber].GetComponent<MiniGame2FlowerInfo>().PlusWaterInfo();
        }

        //*꽃 데이터를 저장한다 */
        void SaveFlowerData()
        {
            for (int i = 0; i < FlowerList.Count; i++)
            {
                PotList[i].WaterInfo = FlowerList[i].GetComponent<MiniGame2FlowerInfo>().WaterInfo;
                PotList[i].FertilizerInfo = FlowerList[i].GetComponent<MiniGame2FlowerInfo>().FertilizerInfo;
                PotList[i].WaterTimer = FlowerList[i].GetComponent<MiniGame2FlowerInfo>().WaterTimer;
                PotList[i].FertilizerTimer = FlowerList[i].GetComponent<MiniGame2FlowerInfo>().FertilizerTimer;
                PotList[i].FlowerStep = FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SavedFlowerStep;
            }
        }
    }
}
