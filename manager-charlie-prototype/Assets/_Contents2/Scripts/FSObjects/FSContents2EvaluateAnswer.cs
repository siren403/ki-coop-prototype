using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2EvaluateAnswer : QnAFiniteState
    {
        private int evalAnswer = 0;
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
            evalAnswer++;

            CDebug.Log(evalAnswer);
            if(evalAnswer == 10)
            {
                evalAnswer = 0;
                Entity.ChangeState(QnAContentsBase.State.Reward);
            }
            else
            {
                Entity.ChangeState(QnAContentsBase.State.Situation);
            }
            CDebug.Log("[FSM] Eval Answer");
            
        }

        public override void Excute()
        {
            //Timer.Update();
            //if(Timer.Check(1.5f))
            //{
            //    Entity.ChangeState(QnAContentsBase.State.Reward);
            //}
        }

        public override void Exit()
        {
 
        }
    }
}
