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
        private GameObject AnswerPanel = null;

        public override void Initialize()
        {
        }
        public override void Enter()
        {
            CDebug.Log("선택지 출력");

            // 데이터를 받아서 답지 출력

            Entity.UI.ShowAnswer();
            CDebug.Log("ShowAnswer : 이미지 확대 + 대사 출력");

            AnswerPanel = GameObject.FindGameObjectWithTag("PanelAnswer");


            Timer.Start();
        }
        public override void Excute()
        {
            Timer.Update();
            if (Timer.Check(3.0f))
            {
                CDebug.Log("After 3.0s, 선택지 출력");

                //* 선택지 패널 이동후 Select 상태 전환 */
                MovePanel();
            }

        }
        public override void Exit()
        {
        }

        public void MovePanel()
        {
            DOTween.Sequence().Append(AnswerPanel.transform.DOMoveY(0, 1.5f));
            Entity.ChangeState(QnAContentsBase.State.Select);
        }
        
    }
}
