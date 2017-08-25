using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;

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
            CDebug.Log("Enter Evaluation");   
            (Entity as SceneContents1).EvaluationConfirm((Entity as SceneContents1).Contents1AnswerNumber);
            
        }

        public override void Excute()
        {
        }

        public override void Exit()
        {
        }
    }
}


