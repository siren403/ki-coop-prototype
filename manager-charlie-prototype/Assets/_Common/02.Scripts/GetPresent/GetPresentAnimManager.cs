using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;

public class GetPresentAnimManager : MonoBehaviour {


    public Transform ImgPresentBox;



    public void AnimStart()
    {
        //* 선물 Box 연출 (임의설정) -> 추후 변경*/
        ImgPresentBox.DORotate(Vector3.one * 10, 0.3f).SetLoops(-1, LoopType.Yoyo);
        ImgPresentBox.DOScale(Vector3.one * 1.5f, 0.2f).SetLoops(-1, LoopType.Yoyo);
    }
}
