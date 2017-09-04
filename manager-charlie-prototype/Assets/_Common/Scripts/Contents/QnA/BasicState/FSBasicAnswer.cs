using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.QnA
{
    public class FSBasicAnswer : QnAFiniteState
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
