using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2ShowAnswer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Answer;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Enter()
        {
            CDebug.Log(" ----------------------------------------------- ShowAnswer----------------------------------");
            Entity.View.ShowAnswer();
            Entity.ChangeState(QnAContentsBase.State.Select);

        }

    }
}
