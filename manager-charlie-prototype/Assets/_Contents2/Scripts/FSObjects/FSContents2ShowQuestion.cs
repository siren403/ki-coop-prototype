using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2ShowQuestion : QnAFiniteState
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
            (Entity as QnAContentsBase).ChangeState(QnAContentsBase.State.Answer);
        }
        public override void Excute()
        {

        }
        public override void Exit()
        {

        }
    }
}