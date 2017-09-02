using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;

namespace Contents3
{
    public class FSContents3Clear : QnAFiniteState
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
                //Outro 활성화
                Entity.View.ShowOutro();
                Entity.ChangeState(QnAContentsBase.State.None);
            }
        }
    }
}
