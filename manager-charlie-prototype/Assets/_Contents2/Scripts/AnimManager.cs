using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;

public class AnimManager : MonoBehaviour {


    // Seq 담당 Panel
    public GameObject PanelHello;
    public GameObject PanelSituation;
    public GameObject PanelQuestion;


    // Animation Objects
    public Transform ImgCharactor;
    public Transform ImgBackground;
    public Transform ImgItem;

    // 조이 Fade In 위치
    Vector3 JoyMoveOn;

    void Awake()
    {
        PanelHello.SetActive(true);
        PanelSituation.SetActive(false);
        PanelQuestion.SetActive(false);
    }

    void Start ()
    {
        StartCoroutine(SeqHello());
    }

    IEnumerator SeqHello()
    {
        JoyMoveOn = new Vector3(200, 100, 0);
        ImgCharactor.DOMove(JoyMoveOn, 0);
        ImgCharactor.DOScale(1.75f, 0);

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(SeqSituation());
    }
    

    IEnumerator SeqSituation()
    {
        PanelSituation.SetActive(true);
        PanelHello.SetActive(false);

        JoyMoveOn = new Vector3(100, 200, 0);
        ImgCharactor.DOMove(JoyMoveOn, 0);
        ImgCharactor.DOScale(1, 0);

        ImgBackground.DOMoveX(ImgBackground.position.x - 400, 1);

        yield return new WaitForSeconds(3.0f);
        ImgCharactor.GetComponent<Image>().DOFade(0, 1);
        

        yield return new WaitForSeconds(1.0f);
        ImgItem.DOScale(2.0f, 1);
        ImgItem.DOMove(Vector3.one * 200, 1);

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(SeqQuestion());
    }

    IEnumerator SeqQuestion()
    {
        PanelQuestion.SetActive(true);
        ImgItem.transform.SetParent(PanelQuestion.transform);
        PanelSituation.SetActive(false);

        yield return new WaitForSeconds(1.0f);
    }


}
