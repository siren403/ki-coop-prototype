using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.QnA
{
    public class FSBasicReward : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Reward;
            }
        }

        public override void Enter()
        {
            Entity.View.ShowReward();
        }
    }
}
