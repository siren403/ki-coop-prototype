using UnityEngine;
using CustomDebug;
using Util;
using Contents;
namespace Examples
{
    public class FSExamShowAnswer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Answer;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("[FSM] Show Answer");
            Entity.UI.ShowAnswer();
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                CDebug.Log("[FSM] Stop Show Answer Aniamtion");
                Entity.ChangeState(QnAContentsBase.State.Select);
            }
        }
        public override void Exit()
        {

        }
    }
}
