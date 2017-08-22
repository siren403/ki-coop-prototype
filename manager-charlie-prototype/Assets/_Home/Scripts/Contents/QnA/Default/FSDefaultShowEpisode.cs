using UnityEngine;
using CustomDebug;
using Util;

namespace Contents
{
    public class FSDefaultShowEpisode : QuestionFiniteState
    {
        public override QnAContantsBase.State StateID
        {
            get
            {
                return QnAContantsBase.State.ShowEpisode;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();
        private float TitleDuration = 3.0f;

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("Start Show Episode");
            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(TitleDuration))
            {
                Entity.ChangeState(QnAContantsBase.State.ShowSituation);
            }
        }
        public override void Exit()
        {
            Entity.ShowEpisodeList();
        }
    }
}
