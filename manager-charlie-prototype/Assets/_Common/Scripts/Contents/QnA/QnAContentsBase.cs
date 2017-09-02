using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;
using System;
using CustomDebug;

namespace Contents.QnA
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

        public abstract IQnAView View { get; }
        protected virtual QnAFiniteState FSEpisode { get { return new FSBasicEpisode(); } }
        protected virtual QnAFiniteState FSSituation { get { return new FSBasicSituation(); } }
        protected virtual QnAFiniteState FSQuestion { get { return EmptyQnAState; } }
        protected virtual QnAFiniteState FSAnswer { get { return new FSBasicAnswer(); } }
        protected virtual QnAFiniteState FSSelect { get { return EmptyQnAState; } }
        protected virtual QnAFiniteState FSEvaluate { get { return EmptyQnAState; } }
        protected virtual QnAFiniteState FSReward { get { return new FSBasicReward(); } }
        protected virtual QnAFiniteState FSClear { get { return new FSBasicClear(); } }


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
            AddState(FSEpisode);
            AddState(FSSituation);
            AddState(FSQuestion);
            AddState(FSAnswer);
            AddState(FSSelect);
            AddState(FSEvaluate);
            AddState(FSReward);
            AddState(FSClear);
        }

        protected sealed override void Start()
        {
            Initialize();
            IViewInitialize viewInit = View as IViewInitialize;
            if(viewInit != null)
            {
                viewInit.Initialize(this);
            }
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

        


    }



}
