using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame2PotClass : MonoBehaviour {

    public int PotID;
    public bool IsEmpty;
    public Vector3 PotPosition;
    public int FlowerInfo;
    public float WarterInfo;
    public float FertilizerInfo;
    public bool IsDead;


    public MiniGame2PotClass(int podId, bool isEmpty,Vector3 potPosition ,int flowerInfo, float warterInfo, float fertilizerInfo, bool isDead)
    {
        PotID = podId;
        IsEmpty = isEmpty;
        PotPosition = potPosition;
        FlowerInfo = flowerInfo;
        WarterInfo = warterInfo;
        FertilizerInfo = fertilizerInfo;
        IsDead = isDead;
    }
}
