using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Contents.QnA
{
    public class FSBasicEpisode : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Episode;
            }
        }

        public override void Enter()
        {
            Entity.View.ShowEpisode();
        }
    }
}
