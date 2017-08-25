using System.Collections;
using System.Collections.Generic;
using Contents.QnA;
using CustomDebug;
using Util;

namespace Contents1
{
    public class FSContents1Question : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Question;
            }
        }


        public override void Initialize()
        {
        }
        public override void Enter()
        {
            CDebug.Log("Question Enter");
            Entity.UI.ShowQuestion();
            //Entity.ChangeState(QnAContentsBase.State.Answer);
        }
        public override void Exit()
        {
        }
    }
}

