using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomDebug;

public class WAnswer : MonoBehaviour {

	
	void Start ()
    {
        StartCoroutine("ComebackSubmission");
	}

    IEnumerator ComebackSubmission()
    {
        CDebug.Log("Oops!");

        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("03.SceneSubmission");
    }
}
