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

        public override void Enter()
        {
            CDebug.Log(" ----------------------------------------------- Select Answer----------------------------------");
            Timer.Check(0.0f);
            Timer.Start();
        }

        public override void Excute()
        {
            Timer.Update();
            if(Timer.Check(10.0f))          //* 10sec waiting, Idle Aniamation play*/
            {
                CDebug.Log("Idle Animation Play , 10 초 이상 반응이 없으면 , UIContent2.cs 에서 SelectAnswer 호출한다");
                Entity.View.HurryUpAnswer();
                    Timer.Check(0.0f);
            }
        }
       
    }
}
