using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Contents;
using DG.Tweening;


public class FSContents3Situation : QnAFiniteState
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

        // 캐릭터를 순서대로 만남.
        // 조이 -> 브루스 -> 토니 (반복)
        

    }
    public override void Exit()
    {

    }
    public override void Excute()
    {

    }
}
