using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomDebug;

public class EpLockManager : MonoBehaviour
{
    //* epsode 버튼들*/
    public Button[] ButtonEp;

    //* epsode 잠금 이미지들*/
    public Image[] ImgEpisodeLock;

    //* PlayerPrefs 에 사용하기 위해 string변수 사용*/
    private string mEpString;

    //* Contents 별로 내가 마지막에 깬 Episode 값 저장 변수*/
    private int mContents1SavedEp;


    // Use this for initialization
    void Start()
    {
        //*초기화. */
        InitEpsode();

        SetClearEpisode();


        //*Ep1은 일단 열어둔다. */
        PlayerPrefs.SetInt("IsEp1Lock" , 1);
        ImgEpisodeLock[0].gameObject.SetActive(false);
        ButtonEp[0].interactable = true;


        //*마지막으로 클리어한 episode 값 가져 온다. (초기값 0)*/
        mContents1SavedEp = PlayerPrefs.GetInt("Contents1MySavedEp");
        CDebug.Log("////  mSavedEpContents1 - > " + mContents1SavedEp);

        //*Ep 별로 clear 한 저장변수 보기 위해*/
        for (int i = 0; i < ButtonEp.Length; i++)
        {
            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            CDebug.Log(" // mEpString  " + (i) + " : " + mEpString);
        }        
    }


    //* 버튼 누르면 다음스테이지가 열림 -> 추후 clear 한 부분에 코드만 이동 */
    public void OnClickSetUnLockEp()
    {
        //*stage 하나 더해지면서 다시 저장*/
        mContents1SavedEp = mContents1SavedEp + 1;
        PlayerPrefs.SetInt("Contents1MySavedEp", mContents1SavedEp);

        //*stage 열림*/
        for (int i = 0; i < mContents1SavedEp+1; i++)
        {
            //* Lock 이미지 제거*/
            ImgEpisodeLock[i].gameObject.SetActive(false);
            //* Button 활성화*/
            ButtonEp[i].interactable = true;

            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            CDebug.Log(" // mEpString  " + (i) + " : " + mEpString);

            PlayerPrefs.SetInt(mEpString , 1);
        }
    }

    /** clear 한 episode에 따라서 episode 잠금 해제 */
    public void SetClearEpisode()
    {
        for (int i = 0; i < ButtonEp.Length; i++)
        {
            int isLock;

            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            isLock = PlayerPrefs.GetInt(mEpString);

            if (isLock == 1)
            {
                ImgEpisodeLock[i].gameObject.SetActive(false);
                ButtonEp[i].interactable = true;
            }
        }
    }

    //*모든 ep 0 으로 초기화 */
    void InitEpsode()
    {
        PlayerPrefs.SetInt("Contents1MySavedEp", 0);
        for (int i = 0; i < ButtonEp.Length; i++)
        {
            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            PlayerPrefs.SetInt(mEpString, 0);
            ButtonEp[i].interactable = false;
        }
    }

    public void OnClickEp1()
    {
        CDebug.Log("///  --- >    Ep 1  Click ! ");
    }
    public void OnClickEp2()
    {
        CDebug.Log("///  --- >    Ep 2  Click ! ");
    }
    public void OnClickEp3()
    {
        CDebug.Log("///  --- >    Ep 3  Click ! ");
    }
    public void OnClickEp4()
    {
        CDebug.Log("///  --- >    Ep 4  Click ! ");
    }
    public void OnClickEp5()
    {
        CDebug.Log("///  --- >    Ep 5  Click ! ");
    }
    public void OnClickEp6()
    {
        CDebug.Log("///  --- >    Ep 6  Click ! ");
    }
}
