using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Examples
{
    public class FSExamSelectAnswer : QnAFiniteState
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

        }
        public override void Excute()
        {
            //10초 대기 등 추후 선택 상태일때 세부구현
        }
        public override void Exit()
        {

        }
    }
}
