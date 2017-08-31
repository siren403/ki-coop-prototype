using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2ShowEpisode : QnAFiniteState
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
            CDebug.Log(" ----------------------------------------------- ShowEpisode----------------------------------");
        }

    }
}
