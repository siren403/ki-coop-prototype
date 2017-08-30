using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Contents.QnA;
using FSM;
using Contents.Data;
using Util.Inspector;
using LitJson;
using System.Linq;
using Util;

namespace Contents1
{
    public class SceneContents1 : QnAContentsBase
    {

        /** 콘텐츠 관련 멤버 */
        private int mSelectedEpisode = 0;       // 유저가 위치하고 있는 에피소드를 체크하는 변수        

        public bool[] BlockInfo = new bool[4] {false, false, false, false};        // 블럭 이미지를 On/Off 하는데 사용되는 정보

        /** UI 및 리소스 관리자 */
        [SerializeField]
        private ViewContents1 mInstUI = null;

        #region Create by seongho

        //데이터 구성요소가 잡히기 전까지 사용할 데이터 객체
        private JsonData mContentsData = null;

        //현재 제출문제의 정답 데이터
        private QuickSheet.Contents1Data mCurrentCorrect = null;
        //현재 제출 선택지 중 유저가 선택한 데이터
        private QuickSheet.Contents1Data mSelectedAnswer = null;
        public QuickSheet.Contents1Data CurrentCorrect
        {
            get
            {
                return mCurrentCorrect;
            }
        }
        public QuickSheet.Contents1Data SelectedAnswer
        {
            get
            {
                return mSelectedAnswer;
            }
        }

        //에피소드 버튼 동적생성을 위한 데이터
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
        public string CurrentPhonics
        {
            get
            {
                return CurrentEpisode["phonics"][mSubmitQuestionCount % CurrentEpisode["phonics"].Count].ToString();
            }
        }

        private Dictionary<string,Queue<QuickSheet.Contents1Data>> mQnA = null;

        private List<int> mIncorrectArr = new List<int>();

        private QuickSheet.Contents1Data[] mAnswers = new QuickSheet.Contents1Data[4];

        //ContentsData로 제어해도 될 듯
        private int mMaximumQuestion = 10;
        private int mSubmitQuestionCount = 0;
        private int mCorrectCount = 0;

        public float CorrectProgress
        {
            get
            {
                return (float)mCorrectCount / mMaximumQuestion;
            }
        }
        //다음 문제가 있는지?
        public bool HasNextQuestion
        {
            get
            {
                return mSubmitQuestionCount < mMaximumQuestion;
            }
        }

        private int mWrongCount = 0;
        public int WrongCount { get { return mWrongCount; } set { mWrongCount = value; } }
        #endregion

        public override IQnAView UI
        {
            get
            {
                return mInstUI;
            }
        }

        protected override void Initialize()
        {            
            string json = Resources.Load<TextAsset>("ContentsData/Contents1").text;
            mContentsData = JsonMapper.ToObject(json);

            ChangeState(State.Episode);
        }

        #region QnAContents를 구성하는 기본적인 State객체
        protected override QnAFiniteState CreateShowEpisode() { return new FSContents1Episode(); }
        protected override QnAFiniteState CreateShowSituation() { return new FSContents1Situation(); }
        protected override QnAFiniteState CreateShowQuestion() { return new FSContents1Question(); }
        protected override QnAFiniteState CreateShowAnswer() { return new FSContents1Answer(); }
        protected override QnAFiniteState CreateShowSelectAnswer() { return new FSContents1Select(); }
        protected override QnAFiniteState CreateShowEvaluateAnswer() { return new FSContents1Evaluation(); }
        protected override QnAFiniteState CreateShowReward() { return new FSContents1Reward(); }
        protected override QnAFiniteState CreateShowClearEpisode() { return new FSContents1Clear(); }
        #endregion


        /**
         * @fn  public void SelectEpisode(int episodeID)
         *
         * @brief   사용자 정의 함수
         *          에피소드 설정 함수
         *
         * @author  Byeongyup
         * @date    2017-08-25
         *
         * @param   episodeID   Identifier for the episode.
         */
        public void SelectEpisode(int episodeID)
        {
            //수정
            mSelectedEpisode = episodeID;
            mIncorrectArr.Clear();

            var table = TableFactory.LoadContents1Table().dataArray
                                    .Where((data) => data.Episode == mSelectedEpisode)
                                    .ToList();

            CDebug.Log("------ Byeong : Debug Start -------");

            // 사용된 단어 걸러내기
            for(int i=0; i<mIncorrectArr.Count; i++)
            {
                CDebug.Log("Jhaneys Comming!");

                if(mIncorrectArr[i] == table[i].ID)
                {
                    CDebug.Log("this word Use!");

                    table.RemoveAt(i);
                }
            }

            // 리스트로 단어 섞기
            int phonicsIndexer = 0;     // 파닉스 알파벳 카운트
            int wordIndexer = 0;       // 단어 카운트
            int tableIndexer = 0;       // 테이블 인덱스 변수

            List<QuickSheet.Contents1Data> words = new List<QuickSheet.Contents1Data>();           // 각 파닉스 별로 추출된 단어를 저장하는 리스트

            CDebug.Log("table length : " + table.Count);

            // 반복문을 사용하여 섞기
            for (int i = 0; i < table.Count; i++)                                                  // 각 에피소드별 파닉스 알파벳 개수를 중심으로
            {
                // 현재 테이블의 Question 값과 현재 추출하고 있는 파닉스 알파벳의 값과 같을 경우
                if (table[i].Question == CurrentEpisode["phonics"][phonicsIndexer % CurrentEpisode["phonics"].Count].ToString())
                {
                    words.Add(table[i]);
                    wordIndexer++;
                }

                if (wordIndexer >= 3)                                                             //  파닉스 알파벳 카운트의 값이 3 이상일 경우
                {
                    ShuffleMachine<List<QuickSheet.Contents1Data>> shuffle = new ShuffleMachine<List<QuickSheet.Contents1Data>>(words);     // 추출된 단어를 저장한 리스트를 보내
                    shuffle.DoShuffle();                                                                                                    // 섞기

                    // 섞인 리스트를 테이블에 다시 저장
                    for (int j = 0; j < words.Count; j++)
                    {
                        table[tableIndexer] = words[j];

                        tableIndexer++;
                    }

                    words.Clear();                                                                  // 배열 초기화

                    phonicsIndexer++;                                                              // 다음 알파벳으로 이동
                    wordIndexer = 0;                                                               // 다음 알파벳의 파닉스 단어를 카운트하기 전 초기화
                }
            }

            // for Debug
            for (int i = 0; i < table.Count; i++)
            {
                CDebug.Log(table[i].Correct);
            }

            CDebug.Log("------ Byeong : Debug End -------");

            mQnA = new Dictionary<string, Queue<QuickSheet.Contents1Data>>();
            
            foreach(var row in table)
            {
                if(mQnA.ContainsKey(row.Question) == false)
                {
                    mQnA.Add(row.Question, new Queue<QuickSheet.Contents1Data>(3));
                }
                mQnA[row.Question].Enqueue(row);
            }

            ChangeState(State.Situation);
        }

        // 출제될 답과 오답들을 가져오는 함수
        public QuickSheet.Contents1Data[] GetAnswers()
        {
            //수정
            for (int i = 0; i < mAnswers.Length; i++)
            {
                mAnswers[i] = null;
            }

            int answersIndex = 0;
            mCurrentCorrect = mQnA[CurrentPhonics].Dequeue();
            mAnswers[answersIndex] = mCurrentCorrect;
            answersIndex++;
            CDebug.LogFormat("CurrentPhonics : {0}", mCurrentCorrect.Question);

            for (int i = 0; i < CurrentEpisode["phonics"].Count; i++)
            {
                if(answersIndex < 4)
                {
                    CDebug.LogFormat("{0}/{1}", CurrentEpisode["phonics"][i].ToString(), mCurrentCorrect.Question);
                    if (CurrentEpisode["phonics"][i].ToString().Equals(mCurrentCorrect.Question) == false)
                    {
                        mAnswers[answersIndex] = mQnA[CurrentEpisode["phonics"][i].ToString()].First();
                        mIncorrectArr.Add(mQnA[CurrentEpisode["phonics"][i].ToString()].First().ID);
                        CDebug.Log("Answers : " + mAnswers[i].Correct);
                        answersIndex++;
                    }
                }
                else
                {
                    break;
                }
            }

            //foreach (var answer in mAnswers)
            //{
            //    if (answer == null)
            //    {
            //        CDebug.Log("answer is null");
            //    }
            //    else
            //    {
            //        CDebug.Log(answer.Correct);
            //    }
            //}

            ShuffleMachine<QuickSheet.Contents1Data[]> shuffle = new ShuffleMachine<QuickSheet.Contents1Data[]>(mAnswers);
            shuffle.DoShuffle();

            //문제 인덱스 증가
            mSubmitQuestionCount++;

            return shuffle.Array;
        }
        //추가
        public void IncrementCorrectCount()
        {
            mCorrectCount++;
        }


        // 선택 안했을 때
        public void NoSelect(int howWait)
        {
            if(howWait >= 5)
            {
                CDebug.Log("Select Please!!!");
            }
            else if( howWait >= 10)
            {
                CDebug.Log("S.e.l.e.t!!!!! P.l.e.a.s.e!!!!!!!!");
            }            
        }


        // 정답 선택 기능 함수
        public void SelectAnswer(int answer)
        {
            mSelectedAnswer = mAnswers[answer];
            ChangeState(State.Evaluation);

            //CDebug.Log("Selection Button : "+ answer);
            //Contents1CorrectNubmer = 0;
            //Contents1AnswerNumber = answer;
            //mInstUI.SelectAnswer();
        }

        // 보상 확인 함수
        public void RewardConfirm()
        {
            CDebug.Log("confirm Reward");
        }                
    }
}


/* ------ 사용하지 않는 코드 ----------*/
////////////////////////////////////////////////////////////////////////////////////////////////////
///** 에피소드 관련 정보 셋팅 */
//void Content1StartGame()
//{
//    //CDebug.Log("hohoho");
//    //Content1CorrectSetting();
//}

/* 각 에피소드에 등장하는 파닉스 단어 */
/* A~E 1~15번, F~J 16~31번, K~O 32~55번, P~S 56~78번, T~Z 79~100번 */
//private const string mEPSODE1 = "ABCDE";
//private const string mEPSODE2 = "FGHIJ";
//private const string mEPSODE3 = "KLMNO";
//private const string mEPSODE4 = "PQRS";
//private const string mEPSODE5 = "TUVWXYZ";

//// Update is called once per frame
//void Update()
//{
//    // 로직 시작
//    if (!mStartEpisode)
//    {
//        mStartEpisode = true;

//        CDebug.Log("hi");

//        // 콘텐츠 시작
//        Content1StartGame();
//    }
//    else
//    {
//        return;
//    }
//}
