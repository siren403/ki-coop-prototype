using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Examples
{
    public class FSExamShowQuestion : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Question;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("[FSM] Show Question and Wating Animation");
            Entity.UI.ShowQuestion();
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                Entity.ChangeState(QnAContentsBase.State.Answer);
            }
        }
        public override void Exit()
        {

        }
    }
}
