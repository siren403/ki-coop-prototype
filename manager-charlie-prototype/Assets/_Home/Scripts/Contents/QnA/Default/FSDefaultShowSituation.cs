using UnityEngine;
using CustomDebug;
using Util;
namespace Contents
{
    public class FSDefaultShowSituation : QuestionFiniteState
    {
        public override QnAContantsBase.State StateID
        {
            get
            {
                return QnAContantsBase.State.ShowSituation;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("Start Situation");
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                Entity.ChangeState(QnAContantsBase.State.None);
            }
        }
        public override void Exit()
        {

        }
    }
}
