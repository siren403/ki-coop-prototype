using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{

    public class FSContents2ShowReward : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Reward;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {
            
        }

        public override void Enter()
        {
            CDebug.Log("[FSM] Show Reward");
            Entity.UI.ShowReward();
            CDebug.Log("[FSM] Send Clear Data");
        }

        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                CDebug.Log("[FSM] Receive Reward Data");
                Entity.UI.ShowReward();
            }
        }

        public override void Exit()
        {
            
        }
    }

}