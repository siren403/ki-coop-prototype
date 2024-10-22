﻿using UnityEngine;
using Contents.QnA;

namespace Contents1
{
    public class FSContents1Episode : QnAFiniteState
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
