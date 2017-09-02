using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2PotClass
{
    public int PotID;
    public bool IsEmpty;
    public int FlowerInfo;
    public int WaterInfo;
    public int FertilizerInfo;
    public bool IsDead;


    public MiniGame2PotClass(int podId, bool isEmpty, int flowerInfo, int waterInfo, int fertilizerInfo, bool isDead)
    {
        PotID = podId;
        IsEmpty = isEmpty;
        FlowerInfo = flowerInfo;
        WaterInfo = waterInfo;
        FertilizerInfo = fertilizerInfo;
        IsDead = isDead;
    }
}


