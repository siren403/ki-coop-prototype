using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2EvaluateAnswer : QnAFiniteState
    {
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
            CDebug.Log("[FSM] Eval Answer");
            Timer.Start();
        }

        public override void Excute()
        {
            Timer.Update();
            if(Timer.Check(1.5f))
            {
                Entity.ChangeState(QnAContentsBase.State.Reward);
            }
        }

        public override void Exit()
        {
 
        }
    }
}
