using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents;

namespace Contents1
{
    public class FSContents1Evaluation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Evaluation;
            }
        }

        public override void Initialize()
        {
        }

        public override void Enter()
        {
            (Entity as SceneContents1).EvaluationConfirm(1);
            
        }

        public override void Excute()
        {
        }

        public override void Exit()
        {
        }
    }
}


