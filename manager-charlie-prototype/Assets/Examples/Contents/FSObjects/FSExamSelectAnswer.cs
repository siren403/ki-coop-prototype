using UnityEngine;
using CustomDebug;
using Util;
using Contents;
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

        }
        public override void Exit()
        {

        }
    }
}
