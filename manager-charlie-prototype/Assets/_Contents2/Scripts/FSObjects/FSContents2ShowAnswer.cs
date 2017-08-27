using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;


public class FSContents2ShowAnswer : QnAFiniteState
{
    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Answer;
        }
    }
    private SimpleTimer Timer = SimpleTimer.Create();

    public override void Initialize()
    {

    }

    public override void Enter()
    {
        Entity.UI.ShowAnswer();
    }

    public override void Excute()
    {
        Timer.Update();
        if(Timer.Check(1.5f))
        {
            CDebug.Log("[FSM] Stop Show Answer Animation");
            Entity.ChangeState(QnAContentsBase.State.Select);
        }
    }

    public override void Exit()
    {

    }
}
