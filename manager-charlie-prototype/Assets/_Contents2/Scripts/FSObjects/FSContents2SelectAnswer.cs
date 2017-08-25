using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Contents2
{
    public class FSContents2SelectAnswer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Select;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {
        }

        public override void Enter()
        {
            Timer.Start();
        }

        public override void Excute()
        {
            

            Timer.Update();
            if(Timer.Check(10.0f))          // 10sec waiting, Idle Aniamation play
            {
                    CDebug.Log("Idle Animation Play");
                    Timer.Check(0.0f);
            }
        }

        public override void Exit()
        {
         
        }
    }
}
