using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using Util.Inspector;
using Contents.QnA;
using CustomDebug;
using Contents.Data;

namespace Contents3
{
    /// <summary>
    /// 컨텐츠3 게임루프 클래스
    /// </summary>
    public class SceneContents3 : QnAContentsBase
    {

        private const int CONTENTS_ID = 3;

        [SerializeField]
        public UIContents3 mInstUI = null;                     // UI 연결용

        public override IQnAView UI
        {
            get
            {
                return mInstUI;
            }
        }


        private string[] mQuestion = new string[] { "Hi", "Hello", "Hey" };
        private int mCurrentQuestion = 0;
        private int mQuestionCount = 0;

        public int SelectAnswerID = 0;

        private JsonData mContentsData = null;

        public int EpisodeCount
        {
            get
            {
                return mContentsData["episode"].Count;
            }
        }

        protected override void Initialize()
        {
            string json = Resources.Load<TextAsset>("ContentsData/Contents3").text;
            mContentsData = JsonMapper.ToObject(json);


            mInstUI.Initialize(this);
            ChangeState(State.Episode);
        }


        protected override QnAFiniteState CreateShowEpisode() { return new FSContents3ShowEpisode(); }
        protected override QnAFiniteState CreateShowSituation() { return new FSContents3ShowSituation(); }
        protected override QnAFiniteState CreateShowQuestion() { return new FSContents3ShowQuestion(); }
        protected override QnAFiniteState CreateShowAnswer() { return new FSContents3ShowAnswer(); }
        protected override QnAFiniteState CreateShowSelectAnswer() { return new FSContents3Select(); }
        protected override QnAFiniteState CreateShowEvaluateAnswer() { return new FSContents3EvaluteAnswer(); }
        protected override QnAFiniteState CreateShowReward() { return new FSContents3ShowReward(); }
        protected override QnAFiniteState CreateShowClearEpisode() { return new FSContents3ClearEpisode(); }



        public void StartEpisode(int episodeID)
        {
            CDebug.Log(string.Format("EpisodeID : {0}", episodeID));
            var table = TableFactory.LoadContents3Table();              // 데이터 로드
            //table.dataArray[0].Correct[0]
            ChangeState(State.Situation);
        }
        public string GetQuestion()
        {
            return "string";
        }
        public string[] GetAnswersData()
        {
            string[] answers = new string[3]
            {
            "Hi", "Hello", "Hey"
            };
            return answers;
        }
        public void SelectAnswer(int answerID)
        {
            this.SelectAnswerID = answerID;
            ChangeState(State.Evaluation);
        }
        public bool Evaluation(int answerID)
        {
            if (answerID == 0)
            {
                return true;
            }
            return false;
        }

        public class QnAContets3
        {
            public string Question;         // 문제
            public string Answer;           // 답변
            public string[] Wrongs;         // 오답
        }













        /*
        void Awake()
        {
            mState = QnAContentsBase.State.None;

            mSituationScript = new SituationDirecting();
            mQuestionScript = new QuestionDirecting();

        }
        */


        /*
        void DoAction()
        {
            switch (mState)
            {
                case QnAContentsBase.State.ShowSituation:
                    {
                        // 상황연출 anim show
                        CDebug.Log("ShowSituation");

                        Instantiate(ObjSituation);     // 상황연출 오브젝트 생성
                        
                    }
                    break;

                case QnAContentsBase.State.ShowQuestion:
                    {
                        // 문제 anim show
                        CDebug.Log("ShowQuestion");

                        Instantiate(ObjQuestion);     // 문제연출 오브젝트 생성

                    }
                    break;

                case QnAContentsBase.State.ShowAnswer:
                    {
                        //if(null == AnswerUI)
                        Instantiate(ObjAnswerUI);// 답변 선택창
                    }
                    break;

                case QnAContentsBase.State.EvaluateAnswer:
                    {
                        // 답변 체크
                        CDebug.Log("EvaluateAnswer");
                    }
                    break;

                case QnAContentsBase.State.QuitQuestion:
                    {
                        CDebug.Log("QuitQuestion");
                    }
                    break;

                case QnAContentsBase.State.ShowReward:
                    {
                        // 보상 anim show
                        CDebug.Log("ShowReward");
                    }
                    break;



            }
        }
        */
        /*
        // FSM 상태 얻는 메소드
        public QnAContentsBase.State GetState()
        {
            return mState;
        }
        // FSM 상태 바꾸는 메소드 
        public void ChangeState(QnAContentsBase.State tState)
        {
            mState = tState;
        }
        */

    }
}
