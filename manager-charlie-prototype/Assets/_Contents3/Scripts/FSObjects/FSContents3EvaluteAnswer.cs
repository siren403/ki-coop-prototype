using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Contents.QnA;
using CustomDebug;
using Contents3;

public class FSContents3EvaluteAnswer : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Evaluation;
        }
    }

    public override void Initialize()
    {

    }
    public override void Enter()
    {
        (Entity.UI as UIContents3).AnswerBlackout();
    }
    public override void Exit()
    {

    }
    public override void Excute()
    {

    }
}
