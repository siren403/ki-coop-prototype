using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using Contents3;
using CustomDebug;

// 버튼 기능 클래스
public class UICanvasBtn {

    private QnAContentsBase.State mState;
    private SceneContents3 mContents3 = null;

    void Awake()
    {
        mContents3 = new SceneContents3();
        mState = mContents3.GetState();
    }

    public void OnClickTestBtn1()
    {
        
    }
    public void OnClickTestBtn2()
    {
        
    }
    
    void CorrectAnswer()
    {
        // if() // 문제 수 체크
        mState = QnAContentsBase.State.ShowQuestion;
        mContents3.ChangeState(mState);
        // else()
        mState = QnAContentsBase.State.ShowReward;
        mContents3.ChangeState(mState);

    }
    void IncorrectAnswer()
    {
        
    }

}
