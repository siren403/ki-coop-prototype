using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;

namespace Contents1
{
    public class FSContents1Answer : QnAFiniteState
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

