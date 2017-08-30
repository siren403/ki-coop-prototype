using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Util;
using DG.Tweening;


namespace Contents3
{
    public class FSContents3ShowQuestion : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Question;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public override void Initialize()
        {

        }
        public override void Enter()
        {
            CDebug.Log("이미지확대 및 대사 출력");

            // 데이터 받아서 문제 출력

            Entity.View.ShowQuestion();
            Timer.Start();

        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(3.0f))
            {
                CDebug.Log("Question Excute : After 3.0f");
                Entity.ChangeState(QnAContentsBase.State.Answer);
            }
        }
        public override void Exit()
        {

        }

    }
}