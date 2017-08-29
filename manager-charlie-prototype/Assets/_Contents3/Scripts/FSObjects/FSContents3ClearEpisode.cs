using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents.QnA;
using CustomDebug;


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

        public override void Initialize()
        {

        }
        public override void Enter()
    {
        CDebug.Log("Enter: ClearEpisode");
        Entity.UI.ClearEpisode();
        Entity.ChangeState(QnAContentsBase.State.None);
    }
        public override void Exit()
        {

        }
        public override void Excute()
        {

        }
    }
}
