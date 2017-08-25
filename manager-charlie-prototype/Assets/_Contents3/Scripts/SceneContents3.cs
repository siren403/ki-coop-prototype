using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Contents;
using CustomDebug;


    /// <summary>
    /// 컨텐츠3의 게임루프 클래스
    /// </summary>
    public class SceneContents3 : QnAContentsBase
    {

        public UIContents InstUI = null;                // UI 연결용
        public override IQnAContentsUI UI
        {
            get 
            {
                return InstUI;
            }
        }


        //private QnAContentsBase Contents = null;
        private QnAContentsBase.State mState;                   // 상태기계 변수

        public GameObject ObjAnswerUI = null;                      // 선택지UI 오브젝트
        

        public GameObject ObjSituation = null;                  // 상황연출 오브젝트
        private SituationDirecting mSituationScript = null;

        public GameObject ObjQuestion = null;                   // 문제제시 오브젝트
        private QuestionDirecting mQuestionScript = null;

        public GameObject ObjReward = null;                     // 리워드 오브젝트

       


        

        void Awake()
        {
            //Contents = new QnAContentsBase();

            mState = QnAContentsBase.State.None;

            mSituationScript = new SituationDirecting();
            mQuestionScript = new QuestionDirecting();

        }



        void Start()
        {


            //AnswerUI = GameObject.FindWithTag("UIAnswer");
            //Debug.Log(AnswerUI);


            //mState = QnAContentsBase.State.ShowQuestion;
            //DoAction();



        }



        void Update()
        {


        }


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

        //** FSM 상태 얻는 메소드 */
        public QnAContentsBase.State GetState()
        {
            return mState;
        }
        //** FSM 상태 바꾸는 메소드 */
        public void ChangeState(QnAContentsBase.State tState)
        {
            mState = tState;
        }



        protected override void Initialize()
        {
            InstUI.Initialize();        // UI Scene 입력

        }


        protected override QnAFiniteState CreateShowAnswer()
        {
            return base.CreateShowAnswer();
        }
        protected override QnAFiniteState CreateShowClearEpisode()
        {
            return base.CreateShowClearEpisode();
        }
        protected override QnAFiniteState CreateShowEvaluateAnswer()
        {
            return base.CreateShowEvaluateAnswer();
        }
        protected override QnAFiniteState CreateShowQuestion()
        {
            return base.CreateShowQuestion();
        }
        protected override QnAFiniteState CreateShowReward()
        {
            return base.CreateShowReward();
        }
        protected override QnAFiniteState CreateShowSelectAnswer()
        {
            return base.CreateShowSelectAnswer();
        }
        protected override QnAFiniteState CreateShowSituation()
        {
            return base.CreateShowSituation();
        }
    }

 