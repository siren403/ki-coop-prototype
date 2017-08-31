using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{

    public class FSContents2ShowSituation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();
        private float duration = 6.0f;

        public override void Enter()
        {
            CDebug.Log(" ----------------------------------------------- ShowSituation----------------------------------");
            CDebug.Log("[FSM] Show Situation and Wating Animation // ");
            Entity.View.ShowSituation();
            Timer.Start();
        }

        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(duration))
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }

    }
}