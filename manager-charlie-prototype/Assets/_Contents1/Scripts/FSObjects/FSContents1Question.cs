using System.Collections;
using System.Collections.Generic;
using Contents;
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
            Entity.UI.ShowQuestion();
            Entity.ChangeState(QnAContentsBase.State.Answer);
        }
        public override void Excute()
        {

        }
        public override void Exit()
        {
        }
    }
}

