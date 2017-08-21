using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class C2_Qexposure : MonoBehaviour
{
    public Transform Target;
    public GameObject NamePanel;
    public Text NameText;
    public GameObject ButtonPanel;

    void Start()
    {
        StartCoroutine("ShowName");
    }

    IEnumerator ShowName()
    {
        Vector3 XY;
        XY.x = 2;
        XY.y = 2;
        XY.z = 1;

        NamePanel.SetActive(false);

        // 이미지 리소스가 도착하면 이미지 리소스로 교체..해야하는데..
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(Target.DOScale(XY, 1));

        yield return new WaitForSeconds(1.5f);
        NamePanel.SetActive(true);
        // 기획서가 도착하면 NameText를 문제로 바꿔줘야할 자리

        yield return new WaitForSeconds(1.5f);
        Debug.Log("Next Scene Set Ready");
        ButtonPanel.SetActive(true);
    }

    public void Buttonclick()
    {
        SceneManager.LoadScene("C2_Submission");
    }
}
