using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Contents.QnA
{
    public class FSBasicSituation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }
        private SimpleTimer mTimer = SimpleTimer.Create();

        public override void Enter()
        {
            mTimer.Start();
            Entity.View.ShowSituation();
        }
        public override void Excute()
        {
            mTimer.Update();
            if (mTimer.Check(1.0f))
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
    }
}
