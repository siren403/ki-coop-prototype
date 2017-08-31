using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Util;

public class FSContents3ShowReward : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Reward;
        }
    }
    private SimpleTimer Timer = SimpleTimer.Create();

    public override void Initialize()
    {

    }
    public override void Enter()
    {

        Entity.View.ShowReward();
        Timer.Start();

    }
    public override void Exit()
    {

    }
    public override void Excute()
    {
        Timer.Update();
        if (Timer.Check(3.0f))
        {
            CDebug.Log("Reward Sticker");   // 나중에 변수 추가하여 전달
            Entity.UI.ShowReward();
        }
    }
}
