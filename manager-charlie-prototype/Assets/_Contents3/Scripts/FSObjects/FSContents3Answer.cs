using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Contents.QnA;
using CustomDebug;

public class FSContents3Answer : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Situation;
        }
    }

    public override void Initialize()
    {

    }
    public override void Enter()
    {
        CDebug.Log("선택지 출력");

        // 데이터를 받아서 답지 출력
        
    }
    public override void Exit()
    {

    }
    public override void Excute()
    {

    }
}
