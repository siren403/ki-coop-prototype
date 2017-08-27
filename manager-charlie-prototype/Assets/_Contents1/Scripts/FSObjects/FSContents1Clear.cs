using System.Collections;
using System.Collections.Generic;
using Contents.QnA;
using CustomDebug;

namespace Contents1
{
    public class FSContents1Clear : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Clear;
            }
        }

        public override void Initialize()
        {
        }

        public override void Enter()
        {
            if((Entity as SceneContents1).CurrentQuestionIndex >= 10 && (Entity as SceneContents1).CorrectAnswerCount >= 10)
            {
                CDebug.Log("10 answer, Great!");
                
            }
            else if((Entity as SceneContents1).CurrentQuestionIndex >= 10)
            {
                CDebug.Log("Clear!");
            }

            Entity.UI.ClearEpisode();
        }

        public override void Excute()
        {
            
        }

        public override void Exit()
        {
        }
    }
}
