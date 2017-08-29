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

        public override void Initialize()
        {

        }

        public override void Enter()
        {

            Timer.Start();

            evalAnswer++;

            CDebug.Log( evalAnswer + "번째 문제");
            CDebug.Log("[FSM] Eval Answer");

            randomCorrectAnswerID = (Entity as SceneContents2).RandomCorrectAnswerID;
            selectedId = (Entity as SceneContents2).SelectedAnswerID;

            CDebug.Log("selectedId  : " + selectedId);

            if (selectedId == randomCorrectAnswerID)
            {
                Entity.UI.CorrectAnswer();
            }
            //* 오답일 때*/
            else
            {
                Entity.UI.WrongAnswer();
                Entity.ChangeState(QnAContentsBase.State.Select);
            }
        }
    

        public override void Excute()
        {
            //Timer.Update();
            //if (Timer.Check(duration))
            //{
            //    //* 정답일 때*/
            //    if (selectedId == randomCorrectAnswerID)
            //    {
            //        Debug.Log("mCorrectCount ->>>> " + evalAnswer);
            //        if (evalAnswer == 10)
            //        {
            //            evalAnswer = 0;
                        
            //            Entity.ChangeState(QnAContentsBase.State.Reward);
            //        }
            //        else
            //        {
            //            Entity.ChangeState(QnAContentsBase.State.Situation);
            //        }
            //    }
            //    //* 오답일 때*/
            //    else
            //    {
                    
            //    }
            //}
        }

        public override void Exit()
        {
 
        }
    }
}
