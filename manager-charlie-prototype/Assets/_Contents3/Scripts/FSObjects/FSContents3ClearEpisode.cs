using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;

namespace Contents3
{
    public class FSContents3ClearEpisode : QnAFiniteState
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
            Entity.View.ClearEpisode();
        }
       
    }
}
