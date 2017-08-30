using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents.QnA;
using Util;
using CustomDebug;
using Contents3;

public class FSContents3SelectAnswer : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Select;
        }
    }

    private SimpleTimer Timer = SimpleTimer.Create();
    private bool AnswerCheck = false;

    public override void Initialize()
    {

    }
    public override void Enter()
    {
        CDebug.Log("Enter: Select");

        Timer.Start();
    }
    public override void Excute()
    {
        //CDebug.Log("Time Checking");
        Timer.Update();
        if (false == AnswerCheck)
        {
            if (Timer.Check(10.0f))
            {
                CDebug.Log("After 10.0s, Don't Answer");
                (Entity.UI as UIContents3).Blackout();

                Requestion();
            }
        }
        else if(true == AnswerCheck)
        {
            if (Timer.Check(3.0f))
            {
                CDebug.Log("After 3.0s, Anim and Change State to Answer");

                // 다시 Answer 진입
                Entity.ChangeState(QnAContentsBase.State.Answer);
                (Entity.UI as UIContents3).HideBalckout();
            }
        }
    }
    public override void Exit()
    {

    }

    public void Requestion()
    {
        // 캐릭터 애니메이션 및 대사 출력
        
        
        Timer.Start();
        AnswerCheck = true;
    }
}
