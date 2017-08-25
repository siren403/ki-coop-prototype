using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using CustomDebug;


public class QuestionDirecting : Contents3Bridge
{

    private GameObject mImgJoy = null;
    private GameObject mImgWe = null;
    private GameObject mImgMis = null;


    private enum JoyWeMis
    {
        Joy, We, Mis
    }
    private JoyWeMis mOrder;


    void Start()
    {
        mImgJoy = Resources.Load("InstJoyAnim") as GameObject;

        mOrder = JoyWeMis.Joy;
        ShowQuestion();

    }


    void Update()
    {

    }

    void ShowQuestion()
    {
        // 문제제시 anim show
        CDebug.Log("ShowQuestion");


        // 조이, 위위, 미스터리가 순서대로 등장하고 문제 제시
        switch (mOrder)
        {
            case JoyWeMis.Joy:
                {
                    CDebug.Log("Joy Q");
                    Instantiate(mImgJoy);

                    mOrder = JoyWeMis.We;
                    CDebug.Log(mOrder);
                }
                break;
            case JoyWeMis.We:
                {
                    CDebug.Log("We Q");

                    mOrder = JoyWeMis.Mis;
                    CDebug.Log(mOrder);
                }
                break;
            case JoyWeMis.Mis:
                {
                    CDebug.Log("Mis Q");

                    mOrder = JoyWeMis.Joy;
                    CDebug.Log(mOrder);
                }
                break;
        }



        // 알렉스 면상 확대
        // 대사 사운드 출력


        //mState = QnAContentsBase.State.ShowAnswer;
        mContents3.ChangeState(mState);
        CDebug.Log(mState);

    }

}
