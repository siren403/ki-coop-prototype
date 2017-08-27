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
        private float mDuration = 3.0f;

        public override void Initialize()
        {
        }
        public override void Enter()
        {
            // 캐릭터를 순서대로 만남.
            // 조이 -> 브루스 -> 토니 (반복)

            CDebug.Log("Situation");
            Timer.Start();

            //Entity.UI.ShowSituation();

        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(mDuration))
            {
                CDebug.Log("Situation Excute : After 3.0f");
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        
        }
        
        public override void Exit()
        {
        }
    }
}