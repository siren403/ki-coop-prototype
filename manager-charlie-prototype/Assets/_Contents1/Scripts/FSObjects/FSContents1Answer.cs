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

        public override void Initialize()
        {
            
        }

        public override void Enter()
        {
            Entity.UI.ShowAnswer();
            //Entity.ChangeState(QnAContentsBase.State.Evaluation);
        }

        public override void Excute()
        {
        }

        public override void Exit()
        {
        }
    }
}

