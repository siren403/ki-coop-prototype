using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Util;


namespace Contents3
{
    public class FSContents3Evaluation : QnAFiniteState
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

            // 정답 비교 
            var scene = Entity as SceneContents3;

            CDebug.Log(scene.CurrentCorrect.ID);
            CDebug.Log(scene.SelectedAnswer.ID);

            if (scene.CurrentCorrect.ID == scene.SelectedAnswer.ID)
            {
                scene.IncrementCorrectCount();
                Entity.View.CorrectAnswer();
            }
            else
            {
                (Entity as SceneContents3).IncrementWrongCount();
                Entity.View.WrongAnswer();
            }
            
        }
       
        
    }
}
