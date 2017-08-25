using UnityEngine;
using CustomDebug;
using Util;
using Contents;
namespace Examples
{
    public class FSExamEvaluateAnswer : QnAFiniteState
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
            if (Timer.Check(1.5f))
            {
                //문제를 다풀었을 경우를 가정
                Entity.ChangeState(QnAContentsBase.State.Reward);
            }
        }
        public override void Exit()
        {

        }
    }
}
