using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2ClearEpisode : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Clear;
            }
        }

        public override void Enter()
        {
            CDebug.Log("[FSM] Clear Episode");
            Entity.View.ClearEpisode();
            Entity.ChangeState(QnAContentsBase.State.None);
        }

    }
}
