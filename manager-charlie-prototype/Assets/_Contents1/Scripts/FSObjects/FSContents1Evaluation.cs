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

            var scene = Entity as SceneContents1;
            if(scene.CurrentCorrect.ID == scene.SelectedAnswer.ID)
            {
                scene.IncrementCorrectCount();
                Entity.UI.CorrectAnswer();
            }
            else
            {
                (Entity as SceneContents1).WrongCount++;
                Entity.UI.WrongAnswer();
            }
        }
        public override void Exit()
        {
        }
    }
}


