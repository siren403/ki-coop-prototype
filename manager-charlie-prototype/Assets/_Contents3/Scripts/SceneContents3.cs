using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Contents.QnA;
using CustomDebug;
using System.Linq;
using Util;
using Util.Inspector;

namespace Contents3
{
    
    /// <summary>
    /// 컨텐츠3 게임루프 클래스
    /// </summary>
    public class SceneContents3 : QnAContentsBase
    {
        
        /** @brief 카테고리의 순회 횟수 (1회차,2회차) */
        private const int TURN_COUNT = 2;

        private int mSelectedEpisode = 0;

        //** UI 및 리소스 관리자 */
        [SerializeField]
        public ViewContents3 mInstUI = null;
        [SerializeField]
        private QuickSheet.Contents3 mQnATable = null;

        public override IQnAView View
        {
            get
            {
                return mInstUI;
            }
        }

        protected override QnAFiniteState FSQuestion { get { return new FSContents3Question(); } }
        protected override QnAFiniteState FSSelect { get { return new FSContents3Select(); } }
        protected override QnAFiniteState FSEvaluate { get { return new FSContents3Evaluation(); } }

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
                return mSubmitQuestionCount < mMaximumQuestion;
            }
        }


        private string[] mCategorys = null;
        /** @brief 최대 문제 수 */
        private int mMaximumQuestion
        {
            get
            {
                return mCategorys.Length * TURN_COUNT;
            }
        }
        /** @brief 제출된 문제 수 */
        private int mSubmitQuestionCount = 0;
        /** @brief 맞은 수 */
        private int mCorrectCount = 0;
        /** @brief 틀린 수 */
        private int mWrongCount = 0;


        public int WrongCount
        {
            get { return mWrongCount; } 
        }
        public int EpisodeCount
        {
            get
            {
                return mQnATable.dataArray.Last().Episode;
            }
        }
        private QuickSheet.Contents3Data[] mAnswers = new QuickSheet.Contents3Data[2];

        public string CurrentCategory
        {
            get
            {
                return mCategorys[mSubmitQuestionCount % mCategorys.Length];
            }
        }

        private Queue<QuickSheet.Contents3Data> mWrongAnswers = null;

        private Dictionary<string, Queue<QuickSheet.Contents3Data>> mQuestionData = new Dictionary<string, Queue<QuickSheet.Contents3Data>>();

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


        protected override void Initialize()
        {
            ChangeState(State.Episode);
        }
       

       /**
        @fn public void SelectEpisode(int episodeID)
       
        @brief  선택한 에피소드에 필요한 데이터를 세팅
                제어구조 별 내용은 SceneContents1 주석 참고
       
        @author SEONG
        @date   2017-09-02
       
        @param  episodeID   Identifier for the episode.
        */
       public void SelectEpisode(int episodeID)
        {
            mSelectedEpisode = episodeID;

            var table = mQnATable.dataArray
                                 .Where((data) => data.Episode == mSelectedEpisode)
                                 .ToList();

            mCategorys = table.Select((data) => data.Category).Distinct().ToArray();

            mQuestionData.Clear();

            foreach(var row in table)
            {
                if(mQuestionData.ContainsKey(row.Category) == false)
                {
                    mQuestionData.Add(row.Category, new Queue<QuickSheet.Contents3Data>());
                }
                mQuestionData[row.Category].Enqueue(row);
            }

            foreach (var category in mCategorys)
            {
                if (mQuestionData.ContainsKey(category))
                {
                    var array = mQuestionData[category].ToArray();
                    array.Shuffle();
                    mQuestionData[category] = new Queue<QuickSheet.Contents3Data>(array);
                }
            }

            int nextEpisodeID = mSelectedEpisode + 1;
            nextEpisodeID = nextEpisodeID > EpisodeCount ? 1 : nextEpisodeID;
            CDebug.LogFormat("Wrong Episode ID : {0}", nextEpisodeID);
            var wrongs = mQnATable.dataArray
                                     .Where((data) => data.Episode == nextEpisodeID)
                                     .ToArray();
            wrongs.Shuffle();
            mWrongAnswers = new Queue<QuickSheet.Contents3Data>(wrongs);

            //var query = from data in mQnATable.dataArray
            //            where (data.Episode == mSelectedEpisode) || (data.Episode == nextEpisodeID)
            //            group data by data.Episode == mSelectedEpisode;
            //foreach(var data in query)
            //{
            //    if (data.Key)
            //    {
            //        foreach(var select in data)
            //        {
            //            CDebug.LogFormat("Select Episode Data {0}", select.Correct);
            //        }
            //    }
            //    else
            //    {
            //        foreach (var next in data)
            //        {
            //            CDebug.LogFormat("Next Episode Data {0}", select.Correct);
            //        }
            //    }
            //}

            ChangeState(State.Situation);
        }

       

        public QuickSheet.Contents3Data[] GetAnswers()
        {
            for (int i = 0; i < mAnswers.Length; i++)
            {
                mAnswers[i] = null;
            }

            int answersIndex = 0;
            mCurrentCorrect = mQuestionData[CurrentCategory].Dequeue();
            mAnswers[answersIndex] = mCurrentCorrect;
            answersIndex++;
            CDebug.LogFormat("CurrentCategory : {0}", mCurrentCorrect.Category);

            mAnswers[answersIndex] = mWrongAnswers.Dequeue();

            mAnswers.Shuffle();

            mSubmitQuestionCount++;

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

        public void RetryEpisode()
        {
            ResetQuestionState();
            SelectEpisode(mSelectedEpisode);
        }
        public void NextEpisode()
        {
            ResetQuestionState();
            mSelectedEpisode = Mathf.Clamp(mSelectedEpisode + 1, 1, EpisodeCount);
            SelectEpisode(mSelectedEpisode);
        }
        private void ResetQuestionState()
        {
            mCurrentCorrect = null;
            mSelectedAnswer = null;
            mSubmitQuestionCount = 0;
            mCorrectCount = 0;
            mWrongCount = 0;
        }
    }
}
