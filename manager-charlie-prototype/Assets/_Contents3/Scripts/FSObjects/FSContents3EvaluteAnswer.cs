using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents.QnA;
using CustomDebug;
using Util;


namespace Contents3
{
    public class FSContents3EvaluteAnswer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Evaluation;
            }
        }
        private int mQuestionCount = 0;

        int randomCorrectAnswerID;

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("Enter: Evaluation");

            // 정답 비교 
            var scene = Entity as SceneContents3;
            if(scene.CurrentCorrect.ID == scene.SelectedAnswer.ID)
            {
                scene.IncreaseCorrectCount();
                Entity.UI.CorrectAnswer();
              
              // 정답 사운드 출력
            }
            else
            {
                (Entity as SceneContents3).WrongCount++;
                Entity.UI.WrongAnswer();
              
              // 오답 사운드 출력
            }
            

            //(Entity.UI as UIContents3).mScene.GetQuestionCount();
            (Entity.View as UIContents3).AnswerBlackout();

            mQuestionCount++;
            CheckFinished();
            

        }
        public override void Exit()
        {
            
        }
        public override void Excute()
        {

        }

        public void CheckFinished()
        {

            CDebug.LogFormat("Question Cnt: {0}", mQuestionCount);
            if (mQuestionCount == 6)
            {
                mQuestionCount = 0;
                Entity.ChangeState(QnAContentsBase.State.Reward);
                 
            }
            else
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
        
    }
}
