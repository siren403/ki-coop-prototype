using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


public class CUIGetGiftPage : MonoBehaviour {


    public GameObject Sticker = null;

    // Use this for initialization
    void Start () {
        CDebug.Log("빵빠레효과");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideSticker()
    {
        Sticker.SetActive(false);
    }

    public void ShowSticker()
    {
        Sticker.SetActive(true);
    }

    public void OnClickBtnGetGift()
    {
       
        ShowSticker();
    }
}
