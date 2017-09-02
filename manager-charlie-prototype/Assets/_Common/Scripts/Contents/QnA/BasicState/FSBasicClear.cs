using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
namespace Contents.QnA
{
    public class FSBasicClear : QnAFiniteState
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

        public override void Excute()
        {
            if (TouchInput.Begin())
            {
                Entity.View.ShowOutro();
                Entity.ChangeState(QnAContentsBase.State.None);
            }
        }
    }
}
