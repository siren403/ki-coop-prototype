using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;
using CustomDebug;

namespace Contents3
{
    public class FSContents3ShowSituation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Situation;
            }
        }

        private SimpleTimer Timer = SimpleTimer.Create();
        private float mDuration = 4.0f;

       
        public override void Enter()
        {
            // 캐릭터를 순서대로 만남.
            // 조이 -> 브루스 -> 토니 (반복)

            CDebug.Log("Enter: Situation ");
            Timer.Start();

            Entity.View.ShowSituation();

        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(mDuration))
            {
                CDebug.Log("Situation Excute : After 4.0f change state to Question");
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
       
    }
}