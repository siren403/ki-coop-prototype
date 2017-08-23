using UnityEngine;
using CustomDebug;
using Util;
using Contents;
namespace Examples
{
    public class FSExamShowSituation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();
        private float Duration = 1.5f;

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("[FSM] Show Situation and Wating Animation");
            Entity.UI.ShowSituation();
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(Duration))
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
        public override void Exit()
        {
        }
    }
}
