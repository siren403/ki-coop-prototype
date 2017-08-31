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

    public bool TestMode;

    // Use this for initialization
    void Start()
    {
        //*태스트를 위해 초기화 해주는 코드 */
        if (TestMode == true)
        {
            InitEpsode();
        }
        

        SetClearEpisode();


        //*Ep1은 일단 열어둔다. */
        PlayerPrefs.SetInt("IsEp1Lock", 1);
        ImgEpisodeLock[0].gameObject.SetActive(false);



        //*마지막으로 클리어한 episode 값 가져 온다. (초기값 0)*/
        mContents1SavedEp = PlayerPrefs.GetInt("Contents1MySavedEp");
        CDebug.Log("////  mSavedEpContents1 - > " + mContents1SavedEp);

        //*Ep 별로 clear 한 저장변수 보기 위해*/
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
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
        for (int i = 0; i < mContents1SavedEp + 1; i++)
        {
            //* Lock 이미지 제거*/
            ImgEpisodeLock[i].gameObject.SetActive(false);
            //* Button 활성화*/
            ButtonEp[i].interactable = true;

            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            CDebug.Log(" // mEpString  " + (i) + " : " + mEpString);

            PlayerPrefs.SetInt(mEpString, 1);
        }
    }

    /** clear 한 episode에 따라서 episode 잠금 해제 */
    void SetClearEpisode()
    {
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
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
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
        {
            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            PlayerPrefs.SetInt(mEpString, 0);
        }
    }

    public void OnClickEp1()
    {
        CDebug.Log("///  --- >    Go Episode 1    ! ");
    }
    public void OnClickEp2()
    {
        //*이전 episode 클리어하지 못하면 unlock 사운드 play*/
        if (mContents1SavedEp < 1)
        {
            CDebug.Log("///  --- >    Ep 2  Lock ! ");
        }
        //*이전 episode 클리어 했다면 그 episode 시작*/
        else
        {
            CDebug.Log("///  --- >   Go Episode 2   ! ");
        }

    }
    public void OnClickEp3()
    {
        if (mContents1SavedEp < 2)
        {
            CDebug.Log("///  --- >    Ep 3  Lock ! ");
        }
        else
        {
            CDebug.Log("///  --- >    Go 3  Episode 3   ! ");
        }
    }
    public void OnClickEp4()
    {
        if (mContents1SavedEp < 3)
        {
            CDebug.Log("///  --- >    Ep 4  Lock ! ");
        }
        else
        {
            CDebug.Log("///  --- >    Go 4  Episode 4   ! ");
        }
    }
    public void OnClickEp5()
    {
        if (mContents1SavedEp < 4)
        {
            CDebug.Log("///  --- >    Ep 5  Lock ! ");
        }
        else
        {
            CDebug.Log("///  --- >     Go 5  Episode 5   !  ");
        }
    }
    public void OnClickEp6()
    {

        CDebug.Log("///  --- >    Go MiniGameScene ");


    }
}
