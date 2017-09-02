using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;

public class MiniGame2FlowerInfo : MonoBehaviour
{

    //*꽃의 정보를 담은 스크립트 */


    //*현재 위치한 화분 번호*/
    public int PotNumber;
    //*꽃 종류*/
    public int FlowerInfo;
    //* 현재 진행 된 물 상태*/
    public int WaterInfo;
    //* 현재 진행 된 비료 상태*/
    public int FertilizerInfo;



    public int WaterTimer;
    public int FertilizerTimer;



    //* 물 부족하고 알려주는 시간*/
    public int CountLackWater;

    //*물 부족할때 죽는 시간*/
    public int DeadTime;
    //* 죽었는지*/
    public bool IsDead;

    //*test 용*/ 
    public Text InstTextWaterInfo;
    public Text InstTextWaterTimer;
    public Text InstTextFlowerStep;
    public Image InstImgLackWater;
    bool OnImgLackWater;

    //*꽃의 단계 설정 */
    enum FlowerStep
    {
        None = 0,

        FlowerStep0, //씨앗
        FlowerStep0LackWater,

        FlowerStep1, //새싹
        FlowerStep1LackWater,

        FlowerStep2, //줄기
        FlowerStep2LackWater,

        FlowerStep3, //꽃봉오리
        FlowerStep3LackWater,

        FlowerStep4,//꽃

        FlowerStep5 //죽음
    }

    FlowerStep flowerStep = FlowerStep.None;
    private void Start()
    {
        InstTextWaterInfo = transform.FindChild("InstTextWaterInfo").GetComponent<Text>();
        InstTextWaterTimer = transform.FindChild("InstTextWaterTimer").GetComponent<Text>();
        InstTextFlowerStep = transform.FindChild("InstTextFlowerStep").GetComponent<Text>();
      
        CDebug.Log(flowerStep);
    }
    public void StartFlowerLife()
    {
        flowerStep = FlowerStep.FlowerStep0;
        StartCoroutine(FlowerLife());
    }
    IEnumerator FlowerLife()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            InstTextWaterInfo.text = "W Info : "+ WaterInfo;
            InstTextWaterTimer.text = "W Timer : " + WaterTimer;
            InstTextFlowerStep.text = "" + flowerStep;
            switch (flowerStep)
            {
                case FlowerStep.None:

                    break;

                //* 처음 심겨졌을 때(씨앗) 상태*/
                case FlowerStep.FlowerStep0:

                    WaterTimer = WaterTimer + 1;
                    if (WaterTimer > CountLackWater)
                    {
                        WaterTimer = 0;
                        flowerStep = FlowerStep.FlowerStep0LackWater;
                    }
                    break;

                case FlowerStep.FlowerStep0LackWater:
                    LackWater();
                    //CDebug.Log(WaterTimer + " 물 부족 " + "("+DeadTime + "초뒤 죽음)");
                    WaterTimer = WaterTimer + 1;
                    CheckDeadTime();
                    break;


                //* 새싹 상태*/
                case FlowerStep.FlowerStep1:
                    CDebug.Log("새싹 상태");
                    WaterTimer = WaterTimer + 1;
                    if (WaterTimer > CountLackWater)
                    {
                        WaterTimer = 0;
                        flowerStep = FlowerStep.FlowerStep1LackWater;
                    }

                    break;

                case FlowerStep.FlowerStep1LackWater:
                    LackWater();
                    //CDebug.Log(WaterTimer + " 물 부족 " + "(" + DeadTime + "초뒤 죽음)");
                    WaterTimer = WaterTimer + 1;
                    CheckDeadTime();
                    break;



                //* 줄기 상태*/
                case FlowerStep.FlowerStep2:
                    CDebug.Log("줄기 상태");
                    WaterTimer = WaterTimer + 1;
                    if (WaterTimer > CountLackWater)
                    {
                        WaterTimer = 0;
                        flowerStep = FlowerStep.FlowerStep2LackWater;
                    }
                    break;

                case FlowerStep.FlowerStep2LackWater:
                    LackWater();
                    //CDebug.Log(WaterTimer + " 물 부족 " + "(" + DeadTime + "초뒤 죽음)");
                    WaterTimer = WaterTimer + 1;
                    CheckDeadTime();
                    break;





                //* 꽃봉오리 상태*/
                case FlowerStep.FlowerStep3:
                    CDebug.Log("꽃봉오리 상태");
                    WaterTimer = WaterTimer + 1;
                    if (WaterTimer > CountLackWater)
                    {
                        WaterTimer = 0;
                        flowerStep = FlowerStep.FlowerStep3LackWater;
                    }
                    break;

                case FlowerStep.FlowerStep3LackWater:
                    LackWater();
                    //CDebug.Log(WaterTimer + " 물 부족 " + "(" + DeadTime + "초뒤 죽음)");
                    WaterTimer = WaterTimer + 1;
                    CheckDeadTime();
                    break;





                //* 꽃 상태*/
                case FlowerStep.FlowerStep4:
                    CDebug.Log("꽃 상태 -> 최종단계" );

                    break;

                case FlowerStep.FlowerStep5:
                    CDebug.Log("죽음");
                    SceneMiniGame2.instance.DeadFlower(PotNumber);
                    InitFlowerInfo();
                    flowerStep = FlowerStep.None;
                    break;
            }
        }
    }

    void CheckDeadTime()
    {
        if (WaterTimer > DeadTime)
        {
            WaterTimer = 0;

            flowerStep = FlowerStep.FlowerStep5;
        }
    }

    //* 물 부족할때 표시해줌*/
    void LackWater()
    {
        if (OnImgLackWater == false)
        {
            CDebug.Log(PotNumber + " 번 꽃 물이 부족합니다 -> 물 부족 이미지 띄워주기");
            InstImgLackWater.GetComponent<Image>().DOFade(1.0f, 1.0f).SetLoops(2, LoopType.Yoyo)
                     .OnStart(() =>
                     {
                         InstImgLackWater.gameObject.SetActive(true);
                         OnImgLackWater = true;
                     })
                     .SetDelay(1.5f)
                     .OnComplete(() =>
                     {
                         InstImgLackWater.gameObject.SetActive(false);

                     });
        }
      
    }

    //* 죽었을 때 호출 -> 화분의 정보를 초기화 한다*/
    void DeadFlower()
    {
        SceneMiniGame2.instance.ErasePotInfo(PotNumber);
    }

    //* 물이나 비료를 줄 때마다 업그레이드 될 수 있는 상태인지 체크를 해준다*/
    public void CheckUpgradeState()
    {
        CDebug.Log("물 , 비료 상태를 보고 업그레이드 할 수 있는지 확인함");
        if (flowerStep == FlowerStep.FlowerStep0 || flowerStep == FlowerStep.FlowerStep0LackWater)
        {
            if (WaterInfo == 1)
            {
                UpgradeStep();
            }
        }
        else if (flowerStep == FlowerStep.FlowerStep1 || flowerStep == FlowerStep.FlowerStep1LackWater)
        {
            if (WaterInfo == 2)
            {
                UpgradeStep();
            }
        }
        else if (flowerStep == FlowerStep.FlowerStep2 || flowerStep == FlowerStep.FlowerStep2LackWater)
        {
            if (WaterInfo == 3)
            {
                UpgradeStep();
            }
        }
        else if (flowerStep == FlowerStep.FlowerStep3 || flowerStep == FlowerStep.FlowerStep3LackWater)
        {
            if (WaterInfo == 4)
            {
                UpgradeStep();
            }
        }
        else if (flowerStep == FlowerStep.FlowerStep4)
        {
            CDebug.Log("마지막 단계 입니다");
        }
    }

    public void UpgradeStep()
    {
        InitFlowerInfo();
        OnImgLackWater = false;
        if (flowerStep == FlowerStep.FlowerStep0 || flowerStep == FlowerStep.FlowerStep0LackWater)
        {
            flowerStep = FlowerStep.FlowerStep1;
        }
        else if (flowerStep == FlowerStep.FlowerStep1 || flowerStep == FlowerStep.FlowerStep1LackWater)
        {
            flowerStep = FlowerStep.FlowerStep2;
        }
        else if (flowerStep == FlowerStep.FlowerStep2 || flowerStep == FlowerStep.FlowerStep2LackWater)
        {
            flowerStep = FlowerStep.FlowerStep3;
        }
        else if (flowerStep == FlowerStep.FlowerStep3 || flowerStep == FlowerStep.FlowerStep3LackWater)
        {
            flowerStep = FlowerStep.FlowerStep4;
        }
    }

    //* 물을 뿌려줄 때 첫번째로 호출 됨 */
    public void PlusWaterInfo()
    {
        WaterInfo = WaterInfo + 1;
        WaterTimer = 0;
        CheckUpgradeState();

        //*물 받았으니 꽃 상태 변경 */
        if (flowerStep == FlowerStep.FlowerStep0 || flowerStep == FlowerStep.FlowerStep0LackWater)
        {
            flowerStep = FlowerStep.FlowerStep0;
        }
        else if (flowerStep == FlowerStep.FlowerStep1 || flowerStep == FlowerStep.FlowerStep1LackWater)
        {
            flowerStep = FlowerStep.FlowerStep1;
        }
        else if (flowerStep == FlowerStep.FlowerStep2 || flowerStep == FlowerStep.FlowerStep2LackWater)
        {
            flowerStep = FlowerStep.FlowerStep2;
        }
        else if (flowerStep == FlowerStep.FlowerStep3 || flowerStep == FlowerStep.FlowerStep3LackWater)
        {
            flowerStep = FlowerStep.FlowerStep3;
        }
    }


    //* 화분에 새로 심어지거나 업그레이드 할 때 호출된다 -> 꽃의 정보를 초기하 한다*/
    public void InitFlowerInfo()
    {
        //*진행된 데이터 초기화*/
        WaterTimer = 0;
        FertilizerTimer = 0;
        WaterInfo = 0;
        FertilizerInfo = 0;
    }

    public void SetFlowerColor(int itemNumber)
    {
        if (itemNumber == 0)
        {
            transform.GetComponent<Image>().color = Color.red;
        }
        else if (itemNumber == 1)
        {
            transform.GetComponent<Image>().color = Color.blue;
        }
        else if (itemNumber == 2)
        {
            transform.GetComponent<Image>().color = Color.yellow;
        }
        else if (itemNumber == 3)
        {
            transform.GetComponent<Image>().color = Color.green;
        }
    }
}



