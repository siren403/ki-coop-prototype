using UnityEngine;
using CustomDebug;
using Util;
using Contents.QnA;

namespace Examples
{
    public class FSExamEvaluateAnswer : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Evaluation;
            }
        }

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("[FSM] Eval Answer");
            if ((Entity as SceneContentsExam).SelectAnswerID == 0)
            {
                (Entity.View as UIContentsExam).CorrectAnswer();
                Entity.ChangeState(QnAContentsBase.State.Reward);
            }
            else
            {
                (Entity.View as UIContentsExam).WrongAnswer();
                Entity.ChangeState(QnAContentsBase.State.Question);
            }
        }
        public override void Exit()
        {

        }
    }
}
