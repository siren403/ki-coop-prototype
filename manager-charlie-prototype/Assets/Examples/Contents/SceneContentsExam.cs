using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;

using System;
using CustomDebug;
using LitJson;
using Util.Inspector;

namespace Examples
{
    /**
     * @class   SceneContentsExam
     *
     * @brief   QnA 컨텐츠의 구현 예제
     *
     * @author  SEONG
     * @date    2017-08-23
     */
    public class SceneContentsExam : QnAContentsBase
    {

        [SerializeField]
        private UIContentsExam mInstUI = null;


        protected override int ContentsID
        {
            get
            {
                return 1;
            }
        }
        /**
         * @property    public UIContentsExam UI
         *
         * @brief   UI 추상화 시 상태 클래스 안에서 접근 할 수 있도록 선언
         *
         * @return  추상화된 UI 인터페이스
         */
        public override IQnAContentsView UI
        {
            get
            {
                return mInstUI;
            }
        }

        private char[] mPhonics = new char[] { 'A', 'B', 'C', 'D', 'E' };
        private int mCurrentPhonics = 0;
        private int mQustionCount = 0;

        public int SelectAnswerID = 0;

        protected override void Initialize()
        {
            mInstUI.Initialize(this);
            ChangeState(State.Episode);
        }
        protected override QnAFiniteState CreateShowEpisode() { return new FSExamShowEpisode(); }
        protected override QnAFiniteState CreateShowSituation() { return new FSExamShowSituation(); }
        protected override QnAFiniteState CreateShowQuestion() { return new FSExamShowQuestion(); }
        protected override QnAFiniteState CreateShowAnswer() { return new FSExamShowAnswer(); }
        protected override QnAFiniteState CreateShowSelectAnswer() { return new FSExamSelectAnswer(); }
        protected override QnAFiniteState CreateShowEvaluateAnswer() { return new FSExamEvaluateAnswer(); }
        protected override QnAFiniteState CreateShowReward() { return new FSExamShowReward(); }
        protected override QnAFiniteState CreateShowClearEpisode() { return new FSExamClearEpisode(); }



        public void StartEpisode(int episodeID)
        {
            CDebug.Log(episodeID);
            ChangeState(State.Situation);
        }
        public char GetPhonics()
        {
            return mPhonics[mCurrentPhonics];
        }
        public string[] GetAnswersData()
        {
            string[] answers = new string[4]
            {
                "Apple","Bread","Carrot","Daikon"
            };
            return answers;
        }
        public void SelectAnswer(int answerID)
        {
            this.SelectAnswerID = answerID;
            ChangeState(State.Evaluation);
            
        }
    }

    public class QnAContents1
    {
        public char Question;
        public string Answer;
        public string[] Wrongs;
    }
}
