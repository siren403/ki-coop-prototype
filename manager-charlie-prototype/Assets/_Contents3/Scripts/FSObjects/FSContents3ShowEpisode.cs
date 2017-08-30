using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents.QnA;
using CustomDebug;

namespace Contents3
{
    public class FSContents3ShowEpisode : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Episode;
            }
        }


        public override void Initialize()
        {

        }
        public override void Enter()
        {
            Entity.View.ShowEpisode();
        }
        public override void Excute()
        {

        }
        public override void Exit()
        {

        }

    }
}