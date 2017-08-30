using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using DG.Tweening;
using UIComponent;

public class Contents2LockSystem : MonoBehaviour {

    public static Contents2LockSystem instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    //* epsode 잠금 이미지들*/
    public Image[] ImgEpisodeLock;

    //* PlayerPrefs 에 사용하기 위해 string변수 사용*/
    private string mEpString;


    //* Contents 별로 내가 마지막에 깬 Episode 값 저장 변수*/
    public int mContentsSavedEp;

    public int Contents2SaveEp
    {
        get
        {
            return mContentsSavedEp;
        }
    }

    public bool TestMode;


    private void Start()
    {
        if (TestMode == true)
        {
            InitEpsode();
        }


        SetClearEpisode();


        //*Ep1은 일단 열어둔다. */
        PlayerPrefs.SetInt("IsEp1Lock", 1);
        ImgEpisodeLock[0].gameObject.SetActive(false);



        //*마지막으로 클리어한 episode 값 가져 온다. (초기값 0)*/
        mContentsSavedEp = PlayerPrefs.GetInt("Contents2MySavedEp");
        CDebug.Log("////  mSavedEpContents2 - > " + mContentsSavedEp);

        //*Ep 별로 clear 한 저장변수 보기 위해*/
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
        {
            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            CDebug.Log(" // mEpString  " + (i) + " : " + mEpString);
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
            }
        }
    }

    public bool CheckClearEpisode(int episodeID)
    {
        bool checkClearEp = false;

        return checkClearEp;
    }

    public void SetClearEpisode(int episodeID)
    {
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
        {
            int isLock;

            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            isLock = PlayerPrefs.GetInt(mEpString);

            if (isLock == 1)
            {
                ImgEpisodeLock[i].gameObject.SetActive(false);
            }
        }
    }

    //*모든 ep 0 으로 초기화 */
    void InitEpsode()
    {
        PlayerPrefs.SetInt("Contents2MySavedEp", 0);
        for (int i = 0; i < ImgEpisodeLock.Length; i++)
        {
            mEpString = System.String.Format("{0}{1}{2} ", "IsEp", i, "Lock");
            PlayerPrefs.SetInt(mEpString, 0);
        }
    }
}
