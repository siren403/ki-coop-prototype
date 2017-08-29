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

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            

            //(Entity.UI as UIContents3).mScene.GetQuestionCount();
            (Entity.UI as UIContents3).AnswerBlackout();
        }
        public override void Exit()
        {

        }
        public override void Excute()
        {

        }

        public void CheckFinished()
        {
            mQuestionCount++;

            CDebug.Log(mQuestionCount);
            if (mQuestionCount == 10)
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
