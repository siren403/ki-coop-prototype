using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Contents.QnA;
using CustomDebug;
using Contents.Data;
using System.Linq;

namespace Contents3
{
    public class QnA
    {
        public int Id;
        public int Episode;
        public string Question;

        public string[] Correct;
        public string Character;


        public QnA(int id, int episode, string question, string[] correct, string character)
        {
            Id = id;
            Episode = episode;
            Question = question;
            Correct = correct;
            Character = character;
        }
    }
    

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

        public override IQnAView View
        {
            get
            {
                return mInstUI;
            }
        }

        #region QnAContents를 구성하는 기본적인 State객체
        protected override QnAFiniteState FSEpisode { get { return new FSContents3ShowEpisode(); } }
        protected override QnAFiniteState FSSituation { get { return new FSContents3ShowSituation(); } }
        protected override QnAFiniteState FSQuestion { get { return new FSContents3ShowQuestion(); } }
        protected override QnAFiniteState FSAnswer { get { return new FSContents3ShowAnswer(); } }
        protected override QnAFiniteState FSSelect { get { return new FSContents3SelectAnswer(); } }
        protected override QnAFiniteState FSEvaluate { get { return new FSContents3EvaluteAnswer(); } }
        protected override QnAFiniteState FSReward { get { return new FSContents3ShowReward(); } }
        protected override QnAFiniteState FSClear { get { return new FSContents3ClearEpisode(); } }
        #endregion

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

        public List<QnA> QnAList = new List<QnA>();

        //* 문제 연출 변수 */
        

        private int mMaximumQuestion = 6;                           // 최대 문제 수
        private int mSubmitQuestionCount = 0;                       // 제출된 문제 수
        private int mCorrectCount = 0;                              // 맞은 수
        private int mWrongCount = 0;                                // 틀린 수

        private int mSetAnswerId = 0;                               // 선택지ID
        private int mRandomCorrectAnswerID = 0; //* 0,1,중 하나 선택 -> 0이면 왼쪽, 1이면 오른쪽에 배치 */

        private List<int> mUsedQuestionID = new List<int>();        // 사용된 문제 번호
        private int SelectAnswerID;

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


        private JsonData mContentsData = null;                          // 데이터 구성요소가 잡히기 전까지 사용할 데이터 객체

        private List<string> mQuestion = new List<string>();              // 사운드로 바뀔 예정
        private string[] mAnswer = new string[] { };

        private Dictionary<string, Queue<QuickSheet.Contents3Data>> mQnA = null;

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
        public string CurrentQuestion
        {
            get
            {
                return CurrentEpisode["question"][mSubmitQuestionCount % CurrentEpisode["question"].Count].ToString();
            }
        }


        //* 데이터 로드 */
        public void GetData(int episodeID)
        {
            mSelectedEpisode = episodeID;
            var table = TableFactory.LoadContents3Table().dataArray
                                    .Where((data) => data.Episode == mSelectedEpisode).ToList();


            foreach (var row in table)
            {
                QnAList.Add(new QnA(row.ID, row.Episode, row.Question, row.Correct, row.Character));
            }
            
          
            mQnA = new Dictionary<string, Queue<QuickSheet.Contents3Data>>();
            foreach (var row in table)
            {
                if (mQnA.ContainsKey(row.Question) == false)
                {
                    mQuestion.Add(row.Question);                    // List에 Question 추가
                    mQnA.Add(row.Question, new Queue<QuickSheet.Contents3Data>(3));
                }
               mQnA[row.Question].Enqueue(row);
            }
        }

        string currentQuestion = "";
        // 출제 문제를 결정
        public void SetQuestion()
        {
            currentQuestion = QnAList[mSubmitQuestionCount % 3].Question;        // 문제 선택 후 플레이 (사운드 예정)
            CDebug.Log(currentQuestion);
        }

        public string GetRightAnswer()
        {
            string Answer = "";
            if (mQnA.ContainsKey(currentQuestion))
            {
                //Answer = mQnA[currentQuestion];
            }


            return Answer;
        }
        public void GetWrongAnswer()
        {

        }

        /*
        public QuickSheet.Contents3Data[] GetAnswers()
        {
            for (int i = 0; i < mAnswers.Length; i++)
            {
                mAnswers[i] = null;
            }
            
            int answersIndex = 0;
            mCurrentCorrect = mQnA["hi"].Dequeue();
            mAnswers[answersIndex] = mCurrentCorrect;

            CDebug.Log(mQnA["hi"]);
            CDebug.Log(mCurrentCorrect);
            //CDebug.LogFormat("CurrentQuestion : {0}", mCurrentCorrect.Question);


            answersIndex++;


            
            for (int i = 0; i < CurrentEpisode["question"].Count; i++)
            {
                if (answersIndex < 2)
                {
                    CDebug.LogFormat("{0}/{1}", CurrentEpisode["question"][i].ToString(), mCurrentCorrect.Question);
                    if (CurrentEpisode["question"][i].ToString().Equals(mCurrentCorrect.Question) == false)
                    {
                        mAnswers[answersIndex] = mQnA[CurrentEpisode["question"][i].ToString()].First();
                        CDebug.Log("Answers : " + mAnswers[i].Correct[i]);
                        answersIndex++;
                    }
                }
                else
                {
                    break;
                }
            }

            mSubmitQuestionCount++;             //문제 인덱스 증가
            
            return mAnswers;
        }
        
            */

        public void IncreaseCorrectCount()
        {
            mCorrectCount++;
        }

        

        

    }
}
