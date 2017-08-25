using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Contents;
using CustomDebug;
using DG.Tweening;


public class FSContents3Question : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Question;
        }
    }


    public override void Initialize()
    {

    }
    public override void Enter()
    {
        CDebug.Log("이미지확대 및 대사 출력");
        
        // 데이터 받아서 문제 출력

    }
    public override void Excute()
    {

    }
    public override void Exit()
    {

    }
    
}
