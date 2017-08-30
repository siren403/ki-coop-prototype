using UnityEngine;
using Contents.QnA;
using Util;

namespace Contents1
{
    public class FSContents1Situation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }
        private SimpleTimer mTimer = SimpleTimer.Create();


        public override void Initialize()
        {
        }
        public override void Enter()
        {
            Entity.View.ShowSituation();
            mTimer.Start();
        }
        public override void Excute()
        {
            mTimer.Update();
            if(mTimer.Check(1.0f))
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
        public override void Exit()
        {
        }
    }
}
