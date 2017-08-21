using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEHManager : MonoBehaviour {

    private string EPSODE1 = "ABCDE";
    private string EPSODE2 = "FGHIJ";
    private string EPSODE3 = "KLMNO";
    private string EPSODE4 = "PQRS";
    private string EPSODE5 = "TUVWXYZ";

    string[] correctAnswer = new string[10];

    GEHData WordsData;

	// Use this for initialization
	void Start () {

        WordsData = new GEHData();

        string a = GEHData.PhanixWords[1].ToString();

        Debug.Log(a);
    }

    void QuestionSetting()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
