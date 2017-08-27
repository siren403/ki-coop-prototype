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
    /** 임의로 생성한 클래스
     * SceneContents에 들어가게 되면 굉장히 지저분해질 것 같아 따로 빼버리기 위해 만든 클래스.
     * 여기서 등장하는 모든 멤버들은 추후에 Json으로 빼든, 뭘 하든 조치가 필요한 코드.
     */
    public class tmpContents1Progress
    {        
        public string[] Episode1Phonics = new string[] {"A", "B", "C", "D", "E"};
        public string[] Episode2Phonics = new string[] {"F", "G", "H", "I", "J"};
        public string[] Episode3Phonics = new string[] {"K", "L", "M", "N", "O"};
        public string[] Episode4Phonics = new string[] {"P", "Q", "R", "S"};
        public string[] Episode5Phonics = new string[] {"T", "U", "V", "W", "X", "Y", "Z"};

        /* A~E 0~14, F~J 15~30, K~O 31~54, P~S 55~77, T~Z 78~99 */

        /** 
        * - 단어에 대한 것
        * 1. 등장하는 파닉스 종류가 에피소드 마다 다 다르고
        * 2. 파닉스 군에 들어가는 단어들의 개수도 다 다르고
        * 
        * - 문제에 대한 것
        * 1. 문제는 10개에
        * 2. 정답에 사용된 단어와 사용되지 않은 단어 구분해야 하고(오답 제외)
        * 3. 같은 파닉스 군에 속한 단어가 중복 등장하면 안되고
        * 4. 각 파닉스 군에 속한 단어가 순서대로 한번씩 등장하였고, 문제 출제 개수가 아직 남았다면, 다시 알파벳 순서대로 반복하고
        */

        /** 
         1. 각 에피소드별 파닉스 구분
         2. 파닉스에 속하는 단어 추출
         3. 추출 된 단어들을 랜덤으로 다시 추출
         4. 랜덤으로 추출된 단어들이 사용되었는지 안되었는지 확인
         5. 사용된 단어라면 다시 추출, 사용되지 않은 단어라면 그대로 진행
         */
    }

    public class SceneContents1 : QnAContentsBase
    {
        private const int CONTENTS_ID = 1;
        private const int MAX_QUESTION_COUNT = 10;

        /** 콘텐츠 관련 멤버 */
        private int mEpisodeLoaction = 0;       // 유저가 위치하고 있는 에피소드를 체크하는 변수        

        private int mCont1AlpahbetCount = 0;          // 알파벳 개수를 저장하는 변수
        private int mCont1WordsCount = 0;             // 알파벳과 관련된 단어 개수를 저장하는 변수

        public int Contents1CorrectNubmer = 0;        // 정답의 인덱스를 저장하는 변수
        public int Contents1AnswerNumber = 0;         // 선택한 정답의 인덱스를 저장하는 변수

        public int CurrentQuestionIndex = 0;         // 문제 진행 개수 체크 변수        
        public int CorrectAnswerCount = 0;           // 정답 맞춘 개수를 카운트 하는 변수
        public int ThisProblemCount = 0;             // 현재 진행하고 있는 문제를 몇 번 풀었는가를 카운트하는 변수

        public float Contents1GuagePercent = 0;      // 게이지 값 변경 변수

        public bool[] BlockInfo = new bool[4] {false, false, false, false};        // 블럭 이미지를 On/Off 하는데 사용되는 정보

        /** UI 및 리소스 관리자 */
        [SerializeField]
        private ViewContents1 mInstUI = null;

        /** 정답, 오답 관련 멤버 */
        private string mCorrectAnswer;          // 정답을 저장하는 변수
        private string[] mInCorrectAnswer = { "A", "A", "A" };      // 오답을 저장하는 배열

        private int[] mUsableWords = new int[10] { 11, 11, 11 , 11, 11, 11, 11, 11, 11, 11 };

        /** 파닉스 단어들을 담고 있는 클래스 -> 추후에 데이터 관련 사항이 정해지면 수정 */
        private Dictionary<int, string> mWords;
        private Dictionary<string, List<string>> mEpisodeWords;
        
        /** 임시로 생성된 클래스, 자세한 설명은 소스 코드 위쪽을 참조 */
        private tmpContents1Progress mProgressData = new tmpContents1Progress();


        #region Create by seongho

        //데이터 구성요소가 잡히기 전까지 사용할 데이터 객체
        private JsonData mContentsData = null;

        ////현재 제출해야할 문제의 파닉스 인덱스
        //private int mPhonicsIndex = 0;

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
                return mContentsData["episode"][mEpisodeLoaction - 1];
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
            //IVewInitialize 구현으로 인해 코드 제거
            //mInstUI.Initialize(this);
            
            string json = Resources.Load<TextAsset>("ContentsData/Contents1").text;
            mContentsData = JsonMapper.ToObject(json);

            //멤버 값 초기화
            mCorrectAnswer = null;

            //mEpisodeLoaction = 1;        // 추후 값 수정 필요
            CorrectAnswerCount = 0;
            CurrentQuestionIndex = 0;

            mWords = new Dictionary<int, string>();
            mEpisodeWords = new Dictionary<string, List<string>>();

            ChangeState(State.Episode);
            //ChangeState(State.Question);
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
         * @brief   사용자(?) 정의 함수 -> 사용자라는 부분을 어떻게 수정해야할지 가이드 부탁드립니다.
         *          에피소드 설정 함수
         *
         * @author  KBY
         * @date    2017-08-25
         *
         * @param   episodeID   Identifier for the episode.
         */
        public void SelectEpisode(int episodeID)
        {
            //수정
            mEpisodeLoaction = episodeID;
            var table = TableFactory.LoadContents1Table().dataArray
                                    .Where((data) => data.Episode == mEpisodeLoaction)
                                    .ToList();
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

        /** 바꿀 게 너무 많습니다.
         *  이 코드를 어디서 부터 어디서 바꿔야 할지...
         */
        public void AnswerSetting()
        {
            //CDebug.Log("Enter Answer setting");

            //string[] answers = new string[4];

            ////var table = this.Container.GetData(CONTENTS_ID)["table"];

            //mWords.Clear();
            //mEpisodeWords.Clear();

            //// 단어 데이터 받아오기
            //for (int i = 0; i < table.Count; i++)
            //{
            //    string word = table[i]["word"].ToString();

            //    mWords.Add(i, word);
            //}

            //// 에피소드1 문제 셋팅
            //if (mEpisodeLoaction == 0)
            //{
            //    // 등장하는 파닉스 알파벳의 개수와, 파닉스에 속하는 단어들의 개수 구하기
            //    int startIndex = 0;                                                         // mWords에 속한 단어들을 검색할 때 사용되는 인덱스 변수
            //    int length = mProgressData.Episode1Phonics.Length;                          // 이번 에피소드에 등장할 단어들의 개수를 가지고 있는 변수
            //    int[] PhonicsWord = new int[length];                                        // 이번 에피소드에 등장할 파닉스 알파벳에 속한 각 단어들의 총 개수를 저장하고 있는 변수 
            //                                                                                // 예) 파닉스 "A"에 속한 단어는 "Almond", "Arcon", "Apple"해서 총 3개. 이것들의 개수를 가지고 있다는 이야기입니다.

            //    //CDebug.Log(length);

            //    int maxCount = 0;                                                           // for문에서 각 단어들의 총 개수를 구할때 사용되는 변수

            //    // 각 파닉스군에 속한 단어들의 개수 구하기
            //    for(int i=0; i<length; i++)
            //    {
            //        maxCount = 0;

            //        for(int j=0; j<mWords.Count; j++)
            //        {
            //            string word = mWords[j];

            //            if (word.Substring(0, 1) == mProgressData.Episode1Phonics[i])
            //            {
            //                maxCount++;
            //            }

            //            CDebug.Log("------ word : " + word);
            //        }

            //        PhonicsWord[i] = maxCount;                                              // 개수 저장
            //        //CDebug.Log("----- maxCount :" + maxCount);
            //    }

            //    // Ep1에서 등장할 단어들을 mEpisodeWord<string, List<string>>에 저장
            //    for (int i = 0; i < length; i++)
            //    {
            //        CDebug.Log("---- Length");

            //        string key = mProgressData.Episode1Phonics[i];

            //        for (int j = 0; j < PhonicsWord[i]; j++)
            //        {           
            //            if(mEpisodeWords.ContainsKey(key) == false)
            //            {
            //                mEpisodeWords.Add(key, new List<string>());
            //            }

            //            mEpisodeWords[key].Add(mWords[startIndex]);
                        
            //            CDebug.Log("----- startIndex : " + startIndex);

            //            startIndex++;
            //        }
            //    }

            //    // 사용할 단어 추출
            //    switch (CurrentQuestionIndex)
            //    {
            //        case 0:
            //        case 5:
            //            break;

            //        case 1:
            //        case 6:
            //            break;
            //        case 2:
            //        case 7:
            //            break;
            //        case 3:
            //        case 8:
            //            break;
            //        case 4:
            //        case 9:
            //            break;
            //    }
            //}

            //mProgressData.Ep1GetAnswers(mWords, CurrentQuestionIndex);
        }

        /**
         * @fn  public string[] GetAnswers()
         *
         * @brief   Gets the answers
         *
         * @author  Byeong
         * @date    2017-08-25
         *
         * @return  An array of string.
         */
        public QuickSheet.Contents1Data[] GetAnswers()
        {
            //수정
            for (int i = 0; i < mAnswers.Length; i++)
                mAnswers[i] = null;

            mCurrentCorrect = mQnA[CurrentPhonics].Dequeue();
            mAnswers[0] = mCurrentCorrect;

            for(int i = 1; i < mAnswers.Length; i++)
            {
                if(CurrentEpisode["phonics"][i].ToString().Equals(mAnswers[0].Question) == false)
                {
                    mAnswers[i] = mQnA[CurrentEpisode["phonics"][i].ToString()].First();
                }
                else
                {
                    continue;
                }
            }

            foreach(var answer in mAnswers)
            {
                if(answer == null)
                {
                    throw new System.Exception("선택지 데이터가 부족함");
                }
            }

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
/** 정답 셋팅 */ //-> 추후에 로직 개선 필요, 마구잡이식으로 짠 코드라 상당히 지저분하고 알아보기가 힘이든다.
             //void Content1CorrectSetting()
             //{
             //    // 임시 지역 변수들
             //    string[] tmpUsableWords = new string[10];   // 사용한 단어들을 저장하는 문자 배열
             //    int tmpStringCount = 0;                     // 카운트 변수

//    // 콘텐츠의 어느 에피소드를 진행하고 있는지에 따라 기본 정보들을 셋팅하는 로직
//    if (mEpisodeLoaction == 1)
//    {
//        // 에피소드 1에 등장하는 단어들을 저장하는 임시 배열들.
//        // 여기에서만 사용되기 때문에 여기에 선언하여 사용. -> 이들을 정리할 수 있는 클래스나 기능이 추가될 경우 로직 수정
//        string[] tmpSaveTextA = new string[3];
//        string[] tmpSaveTextB = new string[3];
//        string[] tmpSaveTextC = new string[3];
//        string[] tmpSaveTextD = new string[3];
//        string[] tmpSaveTextE = new string[3];

//        int[] tmpSettingArr = new int[4];

//        //// string 배열 5개에 이번 에피소드에 등장하는 파닉스 음과 관련한 단어들을 저장하는 로직
//        //// 첫번째 for문-------------
//        //for (int i = 0; i < 5; i++)
//        //{
//        //    tmpStringCount = 0;

//        //    // 중첩, 2번째 for문-----------------
//        //    for (int j = 0; j < mWordsData.Words.Count; j++)
//        //    {
//        //        // i가 0일 경우 'A'와 관련된 단어들을 tmpSaveTextA 배열에 저장
//        //        if (i == 0)
//        //        {
//        //            // 모든 리스트를 검색하기 때문에 일정 요건만 확인하고 벗어남.
//        //            if (tmpStringCount >= 3)
//        //                break;

//        //            if (mWordsData.Words[j].Word.Substring(0, 1) == "A")
//        //            {
//        //                CDebug.Log("I come A");
//        //                tmpSaveTextA[tmpStringCount] = mWordsData.Words[j].Word;
//        //                tmpStringCount++;
//        //            }
//        //        }
//        //        // "B"
//        //        else if (i == 1)
//        //        {
//        //            if (tmpStringCount >= 3)
//        //                break;

//        //            if (mWordsData.Words[j].Word.Substring(0, 1) == "B")
//        //            {
//        //                CDebug.Log("I come B");
//        //                tmpSaveTextB[tmpStringCount] = mWordsData.Words[j].Word;
//        //                tmpStringCount++;
//        //            }
//        //        }
//        //        // "C"
//        //        else if (i == 2)
//        //        {
//        //            if (tmpStringCount >= 3)
//        //                break;

//        //            if (mWordsData.Words[j].Word.Substring(0, 1) == "C")
//        //            {
//        //                CDebug.Log("I come C");
//        //                tmpSaveTextC[tmpStringCount] = mWordsData.Words[j].Word;
//        //                tmpStringCount++;
//        //            }
//        //        }
//        //        // "D"
//        //        else if (i == 3)
//        //        {
//        //            if (tmpStringCount >= 3)
//        //                break;

//        //            if (mWordsData.Words[j].Word.Substring(0, 1) == "D")
//        //            {
//        //                CDebug.Log("I come D");
//        //                tmpSaveTextD[tmpStringCount] = mWordsData.Words[j].Word;
//        //                tmpStringCount++;
//        //            }
//        //        }
//        //        // "E"
//        //        else if (i == 4)
//        //        {
//        //            if (tmpStringCount >= 3)
//        //                break;

//        //            if (mWordsData.Words[j].Word.Substring(0, 1) == "E")
//        //            {
//        //                CDebug.Log("I come E");
//        //                tmpSaveTextE[tmpStringCount] = mWordsData.Words[j].Word;
//        //                tmpStringCount++;
//        //            }
//        //        }
//        //    } // 중첩 for문 종료
//        //} // for문 최종 종료

//        // 출제할 정답과 오답 셋팅
//        switch (mCurrentQuestionIndex)
//        {
//            /* "A" */
//            case 0:
//            case 5:
//                // 정답
//                int tmpNumber = 0;                          // 이 지역에서 쓰이는 임시 랜덤 숫자 저장 변수
//                int[] tmpUsePhanix = { 1, 2, 3, 4 };            // 사용한 파닉스 알파벳을 저장하는 임시 배열
//                string[] tmpUseWord = { "A", "A", "A", "A" };        // 사용한 단어를 저장하는 임시 배열                   

//                tmpNumber = Random.Range(0, 2);

//                mCorrectAnswer = tmpSaveTextA[tmpNumber];

//                tmpStringCount = 0;

//                // 등장할 단어 섞기
//                /* 단어들은 배열에 저장 될 숫자에 따라 결정된다.
//                   단어들은 배열의 위치에 따라 B=[0], C=[1], D=[2], E=[3]인데,
//                   각 배열에 저장되는 숫자가 2, 3, 4, 1이라면 4번을 가지는 [2]번 배열의 요소 D의 단어는 등장하지 않는다.*/
//                for (int i = 0; i < 20; i++)
//                {
//                    int tmpNum1 = Random.Range(0, 3);
//                    int tmpNum2 = Random.Range(0, 3);

//                    if (tmpNum1 == tmpNum2)
//                    {
//                        continue;
//                    }
//                    else
//                    {
//                        int tmpVal = 0;

//                        tmpVal = tmpUsePhanix[tmpNum1];
//                        tmpUsePhanix[tmpNum1] = tmpUsePhanix[tmpNum2];
//                        tmpUsePhanix[tmpNum2] = tmpVal;
//                    }
//                }

//                // 디버그용
//                //for(int i=0; i<4; i++)
//                //{
//                //    CDebug.Log("index : "+tmpUsePhanix[i]);
//                //}

//                tmpStringCount = 0;

//                // 오답
//                for (int i = 0; i < 4; i++)
//                {
//                    if (tmpUsePhanix[i] != 4)
//                    {
//                        if (tmpUsePhanix[i] == 0)
//                        {
//                            mInCorrectAnswer[tmpStringCount] = tmpSaveTextB[Random.Range(0, 2)];
//                            tmpStringCount++;
//                        }
//                        if (tmpUsePhanix[i] == 1)
//                        {
//                            mInCorrectAnswer[tmpStringCount] = tmpSaveTextC[Random.Range(0, 2)];
//                            tmpStringCount++;
//                        }
//                        if (tmpUsePhanix[i] == 2)
//                        {
//                            mInCorrectAnswer[tmpStringCount] = tmpSaveTextD[Random.Range(0, 2)];
//                            tmpStringCount++;
//                        }
//                        if (tmpUsePhanix[i] == 3)
//                        {
//                            mInCorrectAnswer[tmpStringCount] = tmpSaveTextE[Random.Range(0, 2)];
//                            tmpStringCount++;
//                        }
//                    }
//                    else
//                    {
//                        continue;
//                    }

//                }   // for문 종료

//                // 디버그 용
//                //for (int i = 0; i < 3; i++)
//                //{
//                //    CDebug.Log(mInCorrectAnswer[i]);
//                //}

//                break;
//            // "B"
//            case 1:
//            case 6:
//                break;
//            case 2:
//            case 7:
//                break;
//            case 3:
//            case 8:
//                break;
//            case 4:
//            case 9:
//                break;
//        }
//    }
//    else if (mEpisodeLoaction == 2)
//    {

//    }
//    else if (mEpisodeLoaction == 3)
//    {

//    }
//    else if (mEpisodeLoaction == 4)
//    {

//    }
//    else if (mEpisodeLoaction == 5)
//    {

//    }
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
