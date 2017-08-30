using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Contents.QnA;
using LitJson;
using System.Linq;
using Util;
using NUnit.Framework;

namespace Contents1
{
    /**
     @class SceneContents1
    
     @brief 컨텐츠1의 데이터 제어
    
     @author    SEONG
     @date  2017-08-31
     */
    public class SceneContents1 : QnAContentsBase
    {
        /** @brief 파닉스 세트의 순회 횟수 (1회차,2회차) */
        private const int TURN_COUNT = 2;

        /** @brief 유저가 선택한 에피소드 */
        private int mSelectedEpisode = 0;             

        /** @brief View 관리 클래스 */
        [SerializeField]
        private ViewContents1 mInstView = null;

        #region Create by seongho

        //데이터 구성요소가 잡히기 전까지 사용할 데이터 객체
        //private JsonData mContentsData = null;

        /** @brief 현재 제출문제의 정답 데이터 */
        private QuickSheet.Contents1Data mCurrentCorrect = null;
        /** @brief 현재 제출 선택지 중 유저가 선택한 데이터 */
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

        /**
         @property  public int EpisodeCount
        
         @brief 에피소드 버튼의 동적생성을 위한 에피소드 개수
        
         @return    The number of episodes.
         */
        public int EpisodeCount
        {
            get
            {
                //case 1 : 에피소드 아이디를 수집 후 중복을 제거하여 결과의 개수로서 에피소드 개수를 추측
                //return mQnATable.dataArray
                //                .Select((data) => data.Episode)
                //                .Distinct()
                //                .Count();
                               
                //case 2 : 에피소드 순으로 정렬되있다는 가정하에 가장 마지막 데이터로 에피소드 개수를 추측
                return mQnATable.dataArray.Last().Episode;
            }
        }

        /**
         @property  public string CurrentPhonics
        
         @brief 현재 제출되는 문제의 파닉스
        
         @return    The current phonics.
         */
        public string CurrentPhonics
        {
            get
            {
                return mPhonicsSet[mSubmitQuestionCount % mPhonicsSet.Length].ToString();
            }
        }

        /** @brief 현재 에피소드에서 출제 될 문제 */
        private Dictionary<string,Queue<QuickSheet.Contents1Data>> mQuestionData = new Dictionary<string, Queue<QuickSheet.Contents1Data>>();

        //private List<int> mIncorrectArr = new List<int>();

        /** @brief 출제된 문제의 선택지를 담는 배열 */
        private QuickSheet.Contents1Data[] mAnswers = new QuickSheet.Contents1Data[4];

        /**
         @property  private int mMaximumQuestion
        
         @brief 제출할 문제의 최대 갯수
        
         @return    The m maximum question.
         */
        private int mMaximumQuestion
        {
            get
            {
                return mPhonicsSet.Length * TURN_COUNT;
            }
        }
        /** @brief 유저에게 제출한 문제의 개수 */
        private int mSubmitQuestionCount = 0;
        /** @brief 유저가 올바른 정답을 선택한 횟수 */
        private int mCorrectCount = 0;

        /**
         @property  public float CorrectProgress
        
         @brief 올바른 정답에 대한 진행도
        
         @return    The correct progress.
         */
        public float CorrectProgress
        {
            get
            {
                return (float)mCorrectCount / mMaximumQuestion;
            }
        }

        /**
         @property  public bool HasNextQuestion
        
         @brief 다음 문제가 있는지?
        
         @return    True if this object has next question, false if not.
         */
        public bool HasNextQuestion
        {
            get
            {
                return mSubmitQuestionCount < mMaximumQuestion;
            }
        }

        private int mWrongCount = 0;

        /**
         @property  public int WrongCount
        
         @brief 메소드 형태로 제어하는게 좀 더 직관적일 듯
        
         @return    The number of wrongs.
         */
        public int WrongCount { get { return mWrongCount; } }

        /** @brief 테이블 데이터로 추출한 선택한 에피소드의 파닉스 데이터*/
        private string[] mPhonicsSet = null;

        /** @brief 리소스 외부 Attach */
        [SerializeField]
        private QuickSheet.Contents1 mQnATable = null; 
        #endregion

        public override IQnAView View
        {
            get
            {
                return mInstView;
            }
        }

        protected override void Initialize()
        {            
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
            mSelectedEpisode = episodeID;
            //mIncorrectArr.Clear();

            // 테이블을 순회하며 지정한 조건에 해당하는(Where) 데이터를 추출. 리스트로 변환(ToList)
            var table = mQnATable.dataArray
                                 .Where((data) => data.Episode == mSelectedEpisode)
                                 .ToList();

            // 테이블을 순회하며 원하는 데이터를 추출(Select). 중복제거 후(Distinct) 배열로 변환(ToArray)
            mPhonicsSet = table.Select((data) => data.Question).Distinct().ToArray();

            #region Byeong
            //CDebug.Log("------ Byeong : Debug Start -------");

            //// 사용된 단어 걸러내기
            //for(int i=0; i<mIncorrectArr.Count; i++)
            //{
            //    CDebug.Log("Jhaneys Comming!");

            //    if(mIncorrectArr[i] == table[i].ID)
            //    {
            //        CDebug.Log("this word Use!");

            //        table.RemoveAt(i);
            //    }
            //}

            //// 리스트로 단어 섞기
            //int phonicsIndexer = 0;     // 파닉스 알파벳 카운트
            //int wordIndexer = 0;       // 단어 카운트
            //int tableIndexer = 0;       // 테이블 인덱스 변수

            //List<QuickSheet.Contents1Data> words = new List<QuickSheet.Contents1Data>();           // 각 파닉스 별로 추출된 단어를 저장하는 리스트

            //CDebug.Log("table length : " + table.Count);

            //// 반복문을 사용하여 섞기
            //for (int i = 0; i < table.Count; i++)                                                  // 각 에피소드별 파닉스 알파벳 개수를 중심으로
            //{
            //    // 현재 테이블의 Question 값과 현재 추출하고 있는 파닉스 알파벳의 값과 같을 경우
            //    if (table[i].Question == CurrentEpisode["phonics"][phonicsIndexer % CurrentEpisode["phonics"].Count].ToString())
            //    {
            //        words.Add(table[i]);
            //        wordIndexer++;
            //    }

            //    if (wordIndexer >= 3)                                                             //  파닉스 알파벳 카운트의 값이 3 이상일 경우
            //    {
            //        ShuffleMachine<List<QuickSheet.Contents1Data>> shuffle = new ShuffleMachine<List<QuickSheet.Contents1Data>>(words);     // 추출된 단어를 저장한 리스트를 보내
            //        shuffle.DoShuffle();                                                                                                    // 섞기

            //        // 섞인 리스트를 테이블에 다시 저장
            //        for (int j = 0; j < words.Count; j++)
            //        {
            //            table[tableIndexer] = words[j];

            //            tableIndexer++;
            //        }

            //        words.Clear();                                                                  // 배열 초기화

            //        phonicsIndexer++;                                                              // 다음 알파벳으로 이동
            //        wordIndexer = 0;                                                               // 다음 알파벳의 파닉스 단어를 카운트하기 전 초기화
            //    }
            //}

            //// for Debug
            //for (int i = 0; i < table.Count; i++)
            //{
            //    CDebug.Log(table[i].Correct);
            //}

            //CDebug.Log("------ Byeong : Debug End -------");
            #endregion

            // 문제 데이터 초기화
            mQuestionData.Clear();

            //파닉스를 키로 사용하여 해당하는 단어들을 Queue로 분류
            foreach(var row in table)
            {
                if(mQuestionData.ContainsKey(row.Question) == false)
                {
                    mQuestionData.Add(row.Question, new Queue<QuickSheet.Contents1Data>(3));
                }
                mQuestionData[row.Question].Enqueue(row);
            }

            //한번 분류된 Queue데이터를 셔플 후 다시 생성
            foreach(var phonics in mPhonicsSet)
            {
                if (mQuestionData.ContainsKey(phonics))
                {
                    var array = mQuestionData[phonics].ToArray();
                    array.DoShuffle();
                    mQuestionData[phonics] = new Queue<QuickSheet.Contents1Data>(array);
                }
            }
            ChangeState(State.Situation);
        }

        /**
         @fn    public QuickSheet.Contents1Data[] GetAnswers()
        
         @brief 출제될 답과 오답들을 가져오는 함수 (선택지)
        
         @author    SEONG
         @date  2017-08-31
        
         @return    An array of contents 1 data.
         */
        public QuickSheet.Contents1Data[] GetAnswers()
        {
            for (int i = 0; i < mAnswers.Length; i++)
            {
                mAnswers[i] = null;
            }
            // 제출할 선택지를 담을 배열의 인덱스
            int answersIndex = 0;
            mCurrentCorrect = mQuestionData[CurrentPhonics].Dequeue();
            mAnswers[answersIndex] = mCurrentCorrect;
            answersIndex++;
            CDebug.LogFormat("CurrentPhonics : {0}", mCurrentCorrect.Question);

            //string str = "Queue : ";
            //foreach (var question in mQuestionData[CurrentPhonics])
            //{
            //    str += question.Correct + ",";
            //}
            //CDebug.Log(str);

            for (int i = 0; i < mPhonicsSet.Length; i++)
            {
                if(answersIndex < 4)
                {
                    if (mPhonicsSet[i].Equals(mCurrentCorrect.Question) == false)
                    {
                        mAnswers[answersIndex] = mQuestionData[mPhonicsSet[i]].First();
                        //mIncorrectArr.Add(mQuestionData[mPhonicsSet[i]].First().ID);
                        answersIndex++;
                    }
                }
                else
                {
                    break;
                }
            }

            //문제 인덱스 증가
            mSubmitQuestionCount++;

            mAnswers.DoShuffle();
            return mAnswers;
        }
        public void IncrementCorrectCount()
        {
            mCorrectCount++;
        }
        public void IncrementWrongCount()
        {
            mWrongCount++;
        }
        public void ResetWrongCount()
        {
            mWrongCount = 0;
        }
        public void RecycleCurrentQuestion()
        {
            Assert.IsNotNull(mCurrentCorrect, "현재 출제되어 있는 문제가 없습니다.");

            if (mQuestionData.ContainsKey(mCurrentCorrect.Question))
            {
                mQuestionData[mCurrentCorrect.Question].Enqueue(mCurrentCorrect);
            }
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

        /**
         @fn    public void SelectAnswer(int answer)
        
         @brief 정답 선택 
        
         @author    SEONG
         @date  2017-08-31
        
         @param answer  The answer.
         */
        public void SelectAnswer(int answer)
        {
            mSelectedAnswer = mAnswers[answer];
            ChangeState(State.Evaluation);
           
        }

        // 보상 확인 함수
        public void RewardConfirm()
        {
            CDebug.Log("confirm Reward");
        }                
    }
}

