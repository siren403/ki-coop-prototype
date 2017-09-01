using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;
using CustomDebug;

namespace Contents3
{
    public class FSContents3Situation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }

        private SimpleTimer Timer = SimpleTimer.Create();
       
        public override void Enter()
        {
            // 캐릭터를 순서대로 만남.
            // 조이 -> 브루스 -> 토니 (반복)
            Timer.Start();
            Entity.View.ShowSituation();

        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.0f))
            {
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
       
    }
}