using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using CustomDebug;
using LitJson;
using Util;

public class SceneMiniGame2 : MonoBehaviour {

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

    void Start()
    {
        //LoadPotData();
        InitPotDataList();
    }

    //* 화분 정보를 초기화 한다*/
    void InitPotDataList()
    {
        CDebug.Log("저장된 데이터 초기화");
        PotList.Add(new MiniGame2PotClass(0, true, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(1, true, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(2, true, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(3, true, 0, 0, 0, false));
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
        bool isDead;

        for (int i = 0; i < potDataJson.Count; i++)
        {
            potId = int.Parse(potDataJson[i]["PotID"].ToString());
            isEmpty = bool.Parse(potDataJson[i]["IsEmpty"].ToString());
            flowerInfo = int.Parse(potDataJson[i]["FlowerInfo"].ToString());
            waterInfo = int.Parse(potDataJson[i]["WaterInfo"].ToString());
            fertilizerInfo = int.Parse(potDataJson[i]["FertilizerInfo"].ToString());
            isDead = bool.Parse(potDataJson[i]["IsDead"].ToString());

            PotList.Add(new MiniGame2PotClass(potId, isEmpty, flowerInfo, waterInfo, fertilizerInfo, isDead));
        }
        ShowPotList();
    }


    //* 저장된 화분 데이터에 따라서 이미지를 화면에 뿌려준다*/
    void ShowPotList()
    {
        for (int i = 0; i < PotList.Count; i++)
        {
            if (PotList[i].IsEmpty == false)
            {
                CDebug.Log(i + "번째 화분에" + PotList[i].FlowerInfo + " 번 식물 배치");


                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().PotNumber = i;
                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().FlowerInfo = PotList[i].FlowerInfo;

                FlowerList[i].GetComponent<Text>().text = "Flower" + i;
           
            }
        }
    }




    void SavePotData()
    {
        CDebug.Log("데이터 저장 ! ");
        JsonData PotListJson = JsonMapper.ToJson(PotList);
        File.WriteAllText(Application.dataPath + "/_MiniGame2/Resource/PotData.json", PotListJson.ToString());
    }

    //* UIMiniGame2CoinShop 에서 호출됨 */
    public void BuyItem(int itemNumber)
    {
        CDebug.Log(itemNumber + " 번 아이템을 구매하였다 ! ");
        SetFlowerInfo(itemNumber);
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

                FlowerList[i].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;
                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().StartFlowerLife();
                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().PotNumber = i;
                //*전에 있던 데이터 지워준다*/
                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().InitFlowerInfo();
                FlowerList[i].GetComponent<MiniGame2FlowerInfo>().SetFlowerColor(itemNumber);
                break;
            }
            //*화분이 다 찼을 때*/
            if (i == 3)
            {
                CDebug.Log("모든 화분이 다 찼다 -> 1번에 심는다.");
                PotList[0].IsEmpty = false;
                PotList[0].FlowerInfo = itemNumber;


                FlowerList[0].transform.FindChild("Text").GetComponent<Text>().text = "Flower" + i;
                FlowerList[0].GetComponent<MiniGame2FlowerInfo>().StartFlowerLife();
                FlowerList[0].GetComponent<MiniGame2FlowerInfo>().PotNumber = 0;
                //*전에 있던 데이터 지워준다*/
                FlowerList[0].GetComponent<MiniGame2FlowerInfo>().InitFlowerInfo();
                FlowerList[0].GetComponent<MiniGame2FlowerInfo>().SetFlowerColor(itemNumber);
            }
        }
        SavePotData();
    }

    //*지운 꽃의 화분 정보 초기화한다*/
    public void ErasePotInfo(int potNumber)
    {
        PotList[potNumber].PotID = 0;
        PotList[potNumber].IsEmpty = true;
        PotList[potNumber].FlowerInfo = 0;
        PotList[potNumber].WaterInfo = 0;
        PotList[potNumber].FertilizerInfo = 0;
        PotList[potNumber].IsDead = false;

        SavePotData();
    }

    //* 화분 눌렀을 때 호출*/
    public void OnClickBtnPot(int potNumber)
    {
        CDebug.Log("화분 정보 \n" + "PotList[potNumber].PotID : " + PotList[potNumber].PotID + "\n"+  "PotList[potNumber].IsEmpty : " + PotList[potNumber].IsEmpty+ "\n"+
            "PotList[potNumber].FlowerInfo : "+ PotList[potNumber].FlowerInfo + "\n" + "PotList[potNumber].WaterInfo : " + PotList[potNumber].WaterInfo+ "\n"+
            "PotList[potNumber].FertilizerInfo : " + PotList[potNumber].FertilizerInfo +"\n" + "  PotList[potNumber].IsDead : " + PotList[potNumber].IsDead
            );
    }

    public void DeadFlower(int potNumber)
    {
        FlowerList[potNumber].transform.FindChild("Text").GetComponent<Text>().text = "None";
        ErasePotInfo(potNumber);
    }

    public void WaterTheFlower(int potNumber)
    {
        CDebug.Log(potNumber + "번 화분에 물뿌려준다 !");
        FlowerList[potNumber].GetComponent<MiniGame2FlowerInfo>().PlusWaterInfo();
    }
}
