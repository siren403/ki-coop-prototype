using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System;
using CustomDebug;

namespace Contents
{
    /// <summary>
    /// 문제 형식의 컨텐츠구현에 사용하는 클래스
    /// </summary>
    public class QnAContantsBase : ContantsBase<QnAContantsBase,QnAContantsBase.State>
    {
        public enum State
        {
            None, ShowEpisode,
            ShowSituation, ShowQuestion, ShowAnswer, SelectAnswer,
            EvaluateAnswer, QuitQuestion, ShowReward,
        }

        protected State CurrentState = State.None;

        protected override void Initialize()
        {
            AddState(CreateShowEpisode());
            AddState(CreateShowSituation());

            ChangeState(State.ShowEpisode);
        }

        public void ShowEpisodeList()
        {
            CDebug.Log("ShowEpisodeList");
        }

        protected virtual QuestionFiniteState CreateShowEpisode() { return new FSDefaultShowEpisode(); }
        protected virtual QuestionFiniteState CreateShowSituation() { return new FSDefaultShowSituation(); }

    }
    public class QuestionFiniteState : FiniteState<QnAContantsBase, QnAContantsBase.State> { }

    
}
