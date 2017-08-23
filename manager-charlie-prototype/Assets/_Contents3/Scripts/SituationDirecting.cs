using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using Contents3;
using CustomDebug;



public class SituationDirecting : Contents3Bridge
{


	void Start () {


        ShowSituation();
        
	}
	
	void Update () {
		
	}


    void ShowSituation()
    {
        if (QnAContentsBase.State.ShowSituation == mState)
        {

        // 상황연출 anim show
        CDebug.Log("ShowSituation");


        // 알렉스 등장 (웃는 표정으로 손을 들며 인사)
        // 사운드 출력

        // 알렉스 설명 (사운드와 함께 동작)
        // 사운드 출력

        // 알렉스 파이팅 (렛츠런)
        // 사운드 출력


        mState = QnAContentsBase.State.ShowQuestion;
        mContents3.ChangeState(mState);
        CDebug.Log(mState);
     
        }
    }

}
