using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;
using CustomDebug;
using DG.Tweening;

namespace Contents3
{
    public class FSContents3Answer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Answer;
            }
        }

        public override void Enter()
        {
            Entity.View.ShowAnswer();
        }
    }
}
