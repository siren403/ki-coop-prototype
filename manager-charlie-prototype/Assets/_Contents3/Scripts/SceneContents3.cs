using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Contents.QnA;
using CustomDebug;
using Contents.Data;
using System.Linq;

namespace Contents3
{
    /// <summary>
    /// 컨텐츠3 게임루프 클래스
    /// </summary>
    public class SceneContents3 : QnAContentsBase
    {
        private const int CONTENTS_ID = 3;                      // 콘텐츠 번호
        private int mSelectedEpisode = 0;                       // 유저가 위치하고 있는 에피소드

        //** UI 및 리소스 관리자 */
        [SerializeField]
        public UIContents3 mInstUI = null;                      // UI 연결용

        public override IQnAView UI
        {
            get
            {
                return mInstUI;
            }
        }


        private JsonData mContentsData = null;                          // 데이터 구성요소가 잡히기 전까지 사용할 데이터 객체

        private QuickSheet.Contents3Data mCurrentCorrect = null;        // 현재 제출문제의 정답 데이터
        private QuickSheet.Contents3Data mSelectedAnswer = null;        // 현재 제출 선택지 중 유저가 선택한 데이터
        public QuickSheet.Contents3Data CurrentCorrect
        {
            get
            {
                return mCurrentCorrect;
            }
        }
        public QuickSheet.Contents3Data SelectedAnswer
        {
            get
            {
                return mSelectedAnswer;
            }
        }

        public float CorrectProgress
        {
            get
            {
                return (float)mCorrectCount / mMaximumQuestion;
            }
        }
        public bool HasNextQuestion
        {
            get
            {
                return mCorrectCount < mMaximumQuestion;
            }
        }

       

        //* 문제 연출 변수 */
        private string[] mQuestion = new string[] { "Hi", "Hello", "Hey" };              // 사운드로 바뀔 예정
        private string[] mAnswer = new string[] { };

        private int mMaximumQuestion = 6;                           // 최대 문제 수
        private int mSubmitQuestionCount = 0;                       // 제출된 문제 수
        private int mCorrectCount = 0;                              // 맞은 수
        private int mWrongCount = 0;                                // 틀린 수

        private List<int> mUsedQuestionID = new List<int>();        // 사용된 문제 번호
        private int SelectAnswerID;

        private Dictionary<string, Queue<QuickSheet.Contents3Data>> mQnA = null;
        private QuickSheet.Contents3Data[] mAnswers = new QuickSheet.Contents3Data[2];

        public int WrongCount
        {
            get { return mWrongCount; } 
            set { mWrongCount = value; } 
        }

        protected override void Initialize()
        {
            string json = Resources.Load<TextAsset>("ContentsData/Contents3").text;
            mContentsData = JsonMapper.ToObject(json);

            ChangeState(State.Episode);
        }
        public void SelectAnswer(int answerID)
        {
            CDebug.LogFormat("answerID: {0}", answerID);
            this.SelectAnswerID = answerID;
            if (SelectAnswerID == 0)
            {

                ChangeState(State.Evaluation);
            }
        }

        // 에피소드 버튼 동적생성을 위한 데이터
        public int EpisodeCount
        {
            get
            {
                return mContentsData["episode"].Count;
            }
        }
        public JsonData CurrentEpisode
        {
            get
            {
                return mContentsData["episode"][mSelectedEpisode - 1];
            }
        }
       public void SelectEpisode(int episodeID)
        {
            CDebug.Log(string.Format("EpisodeID : {0}", episodeID));

            GetData(episodeID);

            ChangeState(State.Situation);
        }
        public void SetSituation()
        {
            // 문제 상황 캐릭터
            //Character[0] = Resources.Load("Joy") as GameObject;
            //Character[1] = Resources.Load("Joy") as GameObject;
            //Character[2] = Resources.Load("Joy") as GameObject;
        }

        /** 문제 랜덤 생성 */
        public string GetQuestion()
        {
            int i = UnityEngine.Random.Range(0, mQuestion.Length);      // Question의 크기안에서 랜덤으로 문제를 냄

            if(true == IsUsedQuestion(i))
            {
                return GetQuestion();                                          // 다시 랜덤 출제
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
        public string[] GetAnswersData()
        {
            return mAnswer;
        }
        /*
        public QuickSheet.Contents3Data[] GetAnswers()
        {
            for(int i = 0; i < mAnswers.Length; i++)
            {
                mAnswers[i] = null;
            }

            int answersIndex = 0;
            mCurrentCorrect = mQnA[CurrentGreetings].Dequeue();
            mAnswers[answersIndex] = mCurrentCorrect;
            answersIndex++;
            CDebug.LogFormat("Current : {0}", mCurrentCorrect.Question);
            
            for(int i = 0; i < CurrentEpisode["Greeting"].Count; i++)
            {
                if (answersIndex < 4)
                {
                    if (CurrentEpisode["Greeting"][i].ToString().Equals(mCurrentCorrect.Question) == false)
                    {
                        mAnswers[answersIndex] = mQnA[CurrentEpisode["Greeting"][i].ToString()].First();
                        answersIndex++;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        */

        public void IncreaseCorrectCount()
        {
            mCorrectCount++;
        }

        //* 데이터 로드 */
        public void GetData(int episodeID)
        {
            var table = TableFactory.LoadContents3Table().dataArray
                                    .Where((data) => data.Episode == mSelectedEpisode).ToList();

            mQnA = new Dictionary<string, Queue<QuickSheet.Contents3Data>>();
            foreach(var row in table)
            {
                if (mQnA.ContainsKey(row.Question) == false)
                {
                    mQnA.Add(row.Question, new Queue<QuickSheet.Contents3Data>(3));
                }
                mQnA[row.Question].Enqueue(row);
            }
        }


        /** Finite State Machine */
        protected override QnAFiniteState CreateShowEpisode() { return new FSContents3ShowEpisode(); }
        protected override QnAFiniteState CreateShowSituation() { return new FSContents3ShowSituation(); }
        protected override QnAFiniteState CreateShowQuestion() { return new FSContents3ShowQuestion(); }
        protected override QnAFiniteState CreateShowAnswer() { return new FSContents3ShowAnswer(); }
        protected override QnAFiniteState CreateShowSelectAnswer() { return new FSContents3SelectAnswer(); }
        protected override QnAFiniteState CreateShowEvaluateAnswer() { return new FSContents3EvaluteAnswer(); }
        protected override QnAFiniteState CreateShowReward() { return new FSContents3ShowReward(); }
        protected override QnAFiniteState CreateShowClearEpisode() { return new FSContents3ClearEpisode(); }

    }
}
