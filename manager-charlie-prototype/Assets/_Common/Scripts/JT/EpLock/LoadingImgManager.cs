using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;


public class LoadingImgManager : MonoBehaviour {

    public Image ImgLoading;

    
	// Use this for initialization
	void Start () {
        ImgLoading.GetComponent<Image>().DOFade(0,1);
        StartCoroutine(DoActiveFalse());
	}

    IEnumerator DoActiveFalse()
    {
        yield return new WaitForSeconds(1.0f);
        ImgLoading.gameObject.SetActive(false);
    }

}
