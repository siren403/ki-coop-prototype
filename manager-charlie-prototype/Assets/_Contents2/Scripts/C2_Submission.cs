using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class C2_Submission : MonoBehaviour {

    public Transform BackGround;

	void Start ()
    {
        StartCoroutine("ShowSubmission");
	}

    IEnumerator ShowSubmission()
    {
        // 애니메이션 리소스가 오면 Debug를 빼고 애니메이션을 삽입할 자리
        Debug.Log("Animation Start");

        yield return new WaitForSeconds(1.0f);
        Debug.Log("Animation End");

        yield return new WaitForSeconds(1.0f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(BackGround.DOMoveY(400, 1));
    }
}
