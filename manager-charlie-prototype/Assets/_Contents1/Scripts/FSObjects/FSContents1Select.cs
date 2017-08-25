using System.Collections;
using System.Collections.Generic;
using Contents;
using CustomDebug;

namespace Contents1
{
    public class FSContents1Select : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Select;
            }
        }

        public override void Initialize()
        {            
        }

        public override void Enter()
        {
            CDebug.Log("Enter Select");
            Entity.UI.SelectAnswer();
        }

        public override void Excute()
        {
            
        }

        public override void Exit()
        {
        }
    }
}

