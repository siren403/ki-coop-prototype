using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;


public class GetPresentManager : MonoBehaviour {


    public GetPresentAnimManager GetPresentAnimManagerObj;

    // Use this for initialization
    void Start () {

        //* GetPresentAnimManager 의 AnimStart 를 호출해준다. */
        GetPresentAnimManagerObj = GetPresentAnimManagerObj.GetComponent<GetPresentAnimManager>();
        GetPresentAnimManagerObj.AnimStart();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
