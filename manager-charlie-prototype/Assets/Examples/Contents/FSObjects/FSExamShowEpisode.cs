using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Examples
{
    public class FSExamShowEpisode : QnAFiniteState
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
            Entity.UI.ShowEpisode();
        }
        public override void Excute()
        {
        }
        public override void Exit()
        {

        }
    }
}
