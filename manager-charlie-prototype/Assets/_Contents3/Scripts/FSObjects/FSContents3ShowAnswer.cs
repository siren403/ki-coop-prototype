using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using Util;
using CustomDebug;
using DG.Tweening;

namespace Contents3
{
    public class FSContents3ShowAnswer : QnAFiniteState
    {

        public override QnAContentsBase.State StateID
        {
            get
            {
                return QnAContentsBase.State.Answer;
            }
        }
        private SimpleTimer Timer = SimpleTimer.Create();

        public GameObject AnswerPanel = null;

        public override void Initialize()
        {
            
        }
        public override void Enter()
        {
            CDebug.Log("선택지 출력");

            // 데이터를 받아서 답지 출력

            Entity.UI.ShowAnswer();
            AnswerPanel = GameObject.FindGameObjectWithTag("PanelAnswer");
            Timer.Start();

        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(1.5f))
            {
                CDebug.Log("[FSM] Stop Show Answer Aniamtion");
                DOTween.Sequence().Append(AnswerPanel.transform.DOMoveY(200, 1.5f));        // Y축으로 이동
                Entity.ChangeState(QnAContentsBase.State.Select);
            }
        }
        public override void Exit()
        {

        }
    }
}
