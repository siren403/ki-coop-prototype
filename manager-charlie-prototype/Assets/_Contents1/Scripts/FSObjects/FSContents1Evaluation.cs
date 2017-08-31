using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;

namespace Contents1
{
    public class FSContents1Evaluation : QnAFiniteState
    {
        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Evaluation;
            }
        }


        public override void Enter()
        {
            var scene = Entity as SceneContents1;
            if(scene.CurrentCorrect.ID == scene.SelectedAnswer.ID)
            {
                scene.IncrementCorrectCount();
                Entity.View.CorrectAnswer();
            }
            else
            {

                scene.IncrementWrongCount();

                //3번 틀리면 정답강조 후 다음 문제
                if (scene.WrongCount >= 3)
                {
                    scene.RecycleCurrentQuestion();
                    (Entity.View as ViewContents1).PerfectWrongAnswer();
                }
                else
                {
                    Entity.View.WrongAnswer();
                }
            }
        }
     
    }
}


