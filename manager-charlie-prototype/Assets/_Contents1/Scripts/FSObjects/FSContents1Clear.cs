using System.Collections;
using System.Collections.Generic;
using Contents.QnA;
using CustomDebug;
using Util;

namespace Contents1
{
    public class FSContents1Clear : QnAFiniteState
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
