using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Util;
using DG.Tweening;


namespace Contents3
{
    public class FSContents3Question : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Question;
            }
        }

        public override void Enter()
        {
            (Entity as SceneContents3).ResetWrongCount();
            Entity.View.ShowQuestion();
        }
       

    }
}