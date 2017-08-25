using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using CustomDebug;


public class AnswerDirecting : Contents3Bridge {


    public GameObject AnswerBtn = null;


	void Start () {


        CreateAnswer();

	}
	

	void Update () {
		
	}

    void CreateAnswer()
    {
        Vector3 tempVec = new Vector3 (200, 300, 0);
        Instantiate(AnswerBtn, tempVec, Quaternion.identity, this.transform);

        tempVec = new Vector3(200, 100, 0);
        Instantiate(AnswerBtn, tempVec, Quaternion.identity, this.transform);
    }
}
