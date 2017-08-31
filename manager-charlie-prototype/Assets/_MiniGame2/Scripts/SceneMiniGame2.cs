using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using CustomDebug;
using System.IO;
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

        DontDestroyOnLoad(gameObject);
    }

    public List<MiniGame2PotClass> PotList = new List<MiniGame2PotClass>();
    public GameObject FlowerPrefab;
    public GameObject Pots;

    private void Start()
    {
        PotList.Add(new MiniGame2PotClass(0, true, Vector3.one  * 200, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(0, true, Vector3.one, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(0, true, Vector3.one, 0, 0, 0, false));
        PotList.Add(new MiniGame2PotClass(0, true, Vector3.one, 0, 0, 0, false));
    }

    public void SavePotData()
    {
        CDebug.Log("초기화 , 저장되었습니다.");
        for (int i = 0; i < PotList.Count; i++)
        {
            CDebug.Log("PotList[i].IsDead" + PotList[i].IsDead);
        }
    }

    public void BuyItem(int itemNumber)
    {
        CDebug.Log(itemNumber + " 번 아이템을 구매하였다 ! ");

        if (itemNumber == 0)
        {
            PotList[0].IsEmpty = false;
            PotList[0].FlowerInfo = itemNumber;
            PotList[0].WarterInfo = 0;
            PotList[0].FertilizerInfo = 0;
            PotList[0].IsEmpty = false;

            GameObject child = Instantiate(FlowerPrefab) as GameObject;
            child.transform.position = PotList[0].PotPosition;
            child.transform.parent = Pots.transform;
        }
    }


}
