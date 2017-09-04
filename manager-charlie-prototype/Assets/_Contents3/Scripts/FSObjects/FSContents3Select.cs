using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;
using CustomDebug;
using Contents3;

namespace Contents3
{
    public class FSContents3Select : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Select;
            }
        }

        private SimpleTimer mTimer = SimpleTimer.Create();

        public override void Enter()
        {
            mTimer.Start();
        }
        public override void Excute()
        {
            mTimer.Update();
            if (mTimer.Check(10.0f))
            {
                CDebug.Log("After 10.0s, Don't Answer");
            }
        }
        public override void Exit()
        {
            mTimer.Stop();
        }
    }
}
