using UnityEngine;
using CustomDebug;
using Util;
namespace Contents
{
    public class FSDefaultShowSituation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.ShowSituation;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("Start Situation");
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                Entity.ChangeState(QnAContentsBase.State.None);
            }
        }
        public override void Exit()
        {

        }
    }
}
