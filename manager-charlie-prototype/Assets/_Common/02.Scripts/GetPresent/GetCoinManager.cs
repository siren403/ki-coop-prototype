using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;
using DG.Tweening;

public class GetCoinManager : MonoBehaviour {

    
    Sequence SeqImgPresent;

    //* 처음에 나오는 선물 상자*/
    public Image ImgPresentBox;

    //* 받은 선물 관련 메세지 */
    public GameObject Massage;
    
    public Image MassageBack;
    public Text MassageText;
    public float MassageTime;

    //* 받은 선물 이미지*/
    public Image ImgGetPresent;

    // Update is called once per frame
    void Update () {

        
        if (Input.GetMouseButtonDown(0))
        {
            CDebug.Log(" // 터치 한번 입력받으면 코인 얻는 Anim 보여줌 ");
            GetCoin();
        }

    }

    private void OnEnable()
    {
        SeqImgPresent = DOTween.Sequence();

        SeqImgPresent.PrependInterval(0.5f);
        SeqImgPresent.Append(ImgPresentBox.transform.DOScale(1.2f, 0.2f));

        SeqImgPresent.SetLoops(-1, LoopType.Yoyo);
    }
    
    void GetCoin()
    {
        CDebug.Log("동전 날라가는 애니메이션 보여주고 메세지 띄워주기");

        SeqImgPresent.Kill();
        ImgPresentBox.GetComponent<Image>().DOColor(Color.green, 1);
        StartCoroutine(FadeInMassage());
    }

    IEnumerator FadeInMassage()
    {
        CDebug.Log("코인 획득 ! 선물받은 코인 보여줌 ");
        yield return new WaitForSeconds(MassageTime);

        Massage.SetActive(true);
        MassageBack.DOFade(1, 1);
        MassageText.DOFade(1, 1);
        ImgGetPresent.DOFade(1, 1);

        StartCoroutine(FadeOutMassage());

    }


    IEnumerator FadeOutMassage()
    {
        CDebug.Log("몇초뒤 자동으로 fade out");

        yield return new WaitForSeconds(MassageTime);

        MassageBack.DOFade(0, 1);
        MassageText.DOFade(0, 1);
        ImgGetPresent.DOFade(0, 1);

        yield return new WaitForSeconds(1);
        Massage.SetActive(false);
    }

}
