using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System;
using CustomDebug;

namespace Contents
{
    /**
     * @class   QnAContentsBase
     *
     * @brief   문제 형식의 컨텐츠구현에 사용하는 클래스
     *
     * @author  SEONG
     * @date    2017-08-23
     */

    public abstract class QnAContentsBase : ContentsBase<QnAContentsBase,QnAContentsBase.State>
    {
        public static QnAFiniteState EmptyQnAState = new QnAFiniteState();

        public enum State
        {
            None, Episode,
            Situation, Question, Answer, Select,
            Evaluation, Reward, Clear,
        }

        public abstract IQnAContentsUI UI { get; }
        /**
         * @fn  protected sealed override void Awake()
         *
         * @brief   기본적인 시퀀스를 구성하기 위한 상태 초기화
         *
         * @author  SEONG
         * @date    2017-08-23
         */

        protected sealed override void Awake()
        {
            base.Awake();
            AddState(CreateShowEpisode());
            AddState(CreateShowSituation());
            AddState(CreateShowQuestion());
            AddState(CreateShowAnswer());
            AddState(CreateShowSelectAnswer());
            AddState(CreateShowEvaluateAnswer());
            AddState(CreateShowReward());
            AddState(CreateShowClearEpisode());
        }

        protected sealed override void Start()
        {
            Initialize();
        }

        /**
         * @fn  protected abstract void Initialize();
         *
         * @brief   서브클래스에서 초기화 용도로 사용
         *
         * @author  SEONG
         * @date    2017-08-23
         */

        protected abstract void Initialize();

        public void SendStateMessage(params object[] data)
        {
            if(CurrentState != null)
            {
                (CurrentState as QnAFiniteState).ReceiveMessage(data);
            }
        }

        protected virtual QnAFiniteState CreateShowEpisode() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowSituation() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowQuestion() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowAnswer() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowSelectAnswer() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowEvaluateAnswer() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowReward() { return EmptyQnAState; }
        protected virtual QnAFiniteState CreateShowClearEpisode() { return EmptyQnAState; }


    }



}
