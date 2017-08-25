using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents.QnA;
using CustomDebug;


public class FSContents3Episode : QnAFiniteState
{

    public override QnAContentsBase.State StateID
    {
        get
        {
            return QnAContentsBase.State.Episode;
        }
    }


    public override void Initialize()
    {

    }
    public override void Enter()
    {
        Entity.UI.ShowEpisode();

    }
    public override void Excute()
    {

    }
    public override void Exit()
    {
        
    }
   

}
