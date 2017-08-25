using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PageMove : MonoBehaviour {

    public Transform AnswerPage;

	// Use this for initialization
	void Start () {
        AnswerPage.DOMoveY(200, 0.5f);
    }	
	
}
