using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;

public class GetItemManager : MonoBehaviour {


    Sequence SeqImgPresent;
    
    //* 처음에 나오는 선물 상자*/
    public Image ImgPresentBox;


    public int ItemGrade;
    int touchCount;
    int GetPresentCount;

    public GameObject Massage;
    public Text MassageText;
    public Image MassageBack;
    public float MassageTime;
    
    //* 받은 선물 이미지*/
    public Image ImgGetPresent;
    


    //* 받은 선물 이미지*/

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            CDebug.Log(" // 터치 한번 입력받으면 코인 얻는 Anim 보여줌 ");
            SeqImgPresent.Kill();
            GetPresent();
        }

    }

    private void OnEnable()
    {
        GetPresentCount = Random.Range(10, 20);
        Debug.Log("GetPresentCount :  " + GetPresentCount + " 번 터치해야 선물 획득");
        SeqImgPresent = DOTween.Sequence();



        SeqImgPresent.PrependInterval(0.5f);
        SeqImgPresent.Append(ImgPresentBox.transform.DOScale(1.2f, 0.2f));

        SeqImgPresent.SetLoops(-1, LoopType.Yoyo);

    }


    void GetPresent()
    {
        touchCount = touchCount + 1;
        Debug.Log(touchCount);
        ImgPresentBox.DOKill();

        ImgPresentBox.transform.DOScale(Vector3.one, 0);
        ImgPresentBox.transform.DOLocalMoveY(0, 0);

        ImgPresentBox.transform.DOScale(Vector3.one * 1.3f, 0.1f).SetLoops(2, LoopType.Yoyo);
        ImgPresentBox.transform.DOLocalMoveY(40, 0.1f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InBack);

        if (GetPresentCount == touchCount)
        {
            Debug.Log("선물 획득 ! , 선물상자 모션 보여준다 ");
            MassageText.text = "선물 획득 ! ";
            StartCoroutine(FadeInMassage());
        }
    }


    IEnumerator FadeInMassage()
    {
        yield return new WaitForSeconds(MassageTime);

        Massage.SetActive(true);
        MassageBack.DOFade(1, 1);
        MassageText.DOFade(1, 1);
        ImgGetPresent.DOFade(1, 1);

        StartCoroutine(FadeOutMassage());
    }

    IEnumerator FadeOutMassage()
    {
        yield return new WaitForSeconds(MassageTime);

        MassageBack.DOFade(0, 1);
        MassageText.DOFade(0, 1);
        ImgGetPresent.DOFade(0, 1);

        yield return new WaitForSeconds(1);
        Massage.SetActive(false);
    }

}
