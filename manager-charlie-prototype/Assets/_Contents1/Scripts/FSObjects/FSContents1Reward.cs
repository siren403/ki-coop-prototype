using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;

namespace Contents1
{
    public class FSContents1Reward : QnAFiniteState
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
            CDebug.Log("Enter Reward");
            Entity.View.ShowReward();
        }

    }
}

