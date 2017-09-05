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
        private SimpleTimer Timer = SimpleTimer.Create();
        private float duration = 12.0f;

        public override void Enter()
        {
            CDebug.Log(" ----------------------------------------------- ShowQuestion----------------------------------");
            Timer.Start();
            Entity.View.ShowQuestion();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(duration))
            {
                Entity.ChangeState(QnAContentsBase.State.Answer);
            }
        }
       
    }
}