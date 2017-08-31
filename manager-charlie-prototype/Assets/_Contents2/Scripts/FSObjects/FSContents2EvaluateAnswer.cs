using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2EvaluateAnswer : QnAFiniteState
    {
        private int evalAnswer = 0;
        private float duration = 3.0f;

        int randomCorrectAnswerID;
        int selectedId;

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Evaluation;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Enter()
        {
            CDebug.Log(" ----------------------------------------------- EvaluateAnswer----------------------------------");
            Timer.Start();

            evalAnswer++;

            CDebug.Log( evalAnswer + "번째 문제");
            CDebug.Log("[FSM] Eval Answer");

            randomCorrectAnswerID = (Entity as SceneContents2).RandomCorrectAnswerID;
            selectedId = (Entity as SceneContents2).SelectedAnswerID;

            CDebug.Log("selectedId  : " + selectedId);

            if (selectedId == randomCorrectAnswerID)
            {
                Entity.View.CorrectAnswer();
            }
            //* 오답일 때*/
            else
            {
                Entity.View.WrongAnswer();
                Entity.ChangeState(QnAContentsBase.State.Select);
            }
        }
    
    }
}
