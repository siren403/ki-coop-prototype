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
        
        private JsonData mContentsData = null;
    

        //* 문제 연출 변수 */                   
        private string[] mQuestion = new string[] { };
        private string[] mAnswer = new string[] { };

        private int mQuestionCount = 0;                             // 등장 문제 수
        private List<int> mUsedQuestionID = new List<int>();        // 사용된 문제 번호
        private int SelectAnswerID;



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


            ChangeState(State.Episode);
        }


        protected override QnAFiniteState CreateShowEpisode() { return new FSContents3ShowEpisode(); }
        protected override QnAFiniteState CreateShowSituation() { return new FSContents3ShowSituation(); }
        protected override QnAFiniteState CreateShowQuestion() { return new FSContents3ShowQuestion(); }
        protected override QnAFiniteState CreateShowAnswer() { return new FSContents3ShowAnswer(); }
        protected override QnAFiniteState CreateShowSelectAnswer() { return new FSContents3SelectAnswer(); }
        protected override QnAFiniteState CreateShowEvaluateAnswer() { return new FSContents3EvaluteAnswer(); }
        protected override QnAFiniteState CreateShowReward() { return new FSContents3ShowReward(); }
        protected override QnAFiniteState CreateShowClearEpisode() { return new FSContents3ClearEpisode(); }



        public void StartEpisode(int episodeID)
        {
            CDebug.Log(string.Format("EpisodeID : {0}", episodeID));

            //GetData();

            ChangeState(State.Situation);
        }
        public void SetSituation()
        {
            // 문제상황 캐릭터
            //Character[0] = Resources.Load("Joy") as GameObject;
            //Character[1] = Resources.Load("Joy") as GameObject;
            //Character[2] = Resources.Load("Joy") as GameObject;
        }
        public string GetQuestion()
        {
            int i = UnityEngine.Random.Range(0, mQuestion.Length);      // Question의 크기안에서 랜덤으로 문제를 냄

            if(true == IsUsedQuestion(i))
            {
                return GetQuestion();                                   // 다시 랜덤 출제
            }
            else
            {
                mUsedQuestionID.Add(i);                                 // 이미낸 문제는 나중에 거르기 위해 List에 저장
                return mQuestion[i];                                    // 중복 안된 것을 출제
            }
        }
        public bool IsUsedQuestion(int i)
        {
            for (int j = 0; j < mQuestion.Length; j++)
            {
                if (mQuestion[i] == mQuestion[mUsedQuestionID[j]])          // 선택된 문제가 이미 사용됨 = true
                {
                    return true;                                   
                }
                else if (mQuestion[i] != mQuestion[mUsedQuestionID[j]])     // 선택된 문제가 사용 안됨 = false
                {
                    return false;
                }
            }
            return false;
        }
        public bool IsFinished()
        {
            if (6 == mQuestionCount)
            {
                CDebug.Log("Finished");
                ChangeState(QnAContentsBase.State.Reward);
            }
            return false;
        }
        public string[] GetAnswersData()
        {
            //string[] answers = new string[3]
            //{
            //"Hi", "Hello", "Hey"
            //};
            return mAnswer;
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

        //* 데이터 로드 */
        public void GetData()
        {
            var table = TableFactory.LoadContents3Table();
            for (int i = 0; i < table.dataArray.Length; i++)
            {
                mQuestion[i] = table.dataArray[i].Question;
                CDebug.Log(mQuestion[i]);

                for (int j = 0; j < 3; j++)
                {
                    mAnswer[i] = table.dataArray[i].Correct[j];
                    CDebug.Log(mAnswer[j]);
                }
            }
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
