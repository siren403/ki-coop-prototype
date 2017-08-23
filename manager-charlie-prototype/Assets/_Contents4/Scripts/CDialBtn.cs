using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


public class CDialBtn : MonoBehaviour {
    
     
	// Use this for initialization
	void Start () {
		
	}
	

	// Update is called once per frame
	void Update () {

        //Click();
	}

    public void Click()
    {
        if(Input.GetMouseButtonDown(0) == true)
        {
            CDebug.Log("asd");
        }
    }


}
