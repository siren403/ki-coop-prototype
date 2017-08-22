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
    public class QnAContentsBase : ContentsBase<QnAContentsBase,QnAContentsBase.State>
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

        protected virtual QnAFiniteState CreateShowEpisode() { return new FSDefaultShowEpisode(); }
        protected virtual QnAFiniteState CreateShowSituation() { return new FSDefaultShowSituation(); }

    }
    

    
}
