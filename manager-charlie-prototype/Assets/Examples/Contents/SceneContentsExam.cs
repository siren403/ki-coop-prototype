using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using CustomDebug;
using Contents.Data;
using QuickSheet;
using Util.Inspector;
using Util;

namespace Examples
{
    [System.Serializable]
    public class ContentsData
    {
        public List<EpisodeData> episode = null;
    }
    [System.Serializable]
    public class EpisodeData
    {
        public string[] phonics = null;
    }

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

        /**
         * @property    public UIContentsExam UI
         *
         * @brief   UI 추상화 시 상태 클래스 안에서 접근 할 수 있도록 선언
         *
         * @return  추상화된 UI 인터페이스
         */
        public override IQnAView UI
        {
            get
            {
                return mInstUI;
            }
        }

        private QuickSheet.Contents1 mQnATable = null;
        private ContentsData mContentsData = null;
        private int mSelectEpisode = 0;

       
        private int mQustionCount = 0;
        private int mPhonicsIndex = 0;

        public int SelectAnswerID = 0;

        public int EpisodeCount
        {
            get
            {
                return mContentsData.episode.Count;
            }
        }
        public EpisodeData CurrentEpisode
        {
            get
            {
                return mContentsData.episode[mSelectEpisode];
            }
        }
        public string CurrentPhonics
        {
            get
            {
                return CurrentEpisode.phonics[mPhonicsIndex % CurrentEpisode.phonics.Length];
            }
        }
        protected override void Initialize()
        {
            string json = Resources.Load<TextAsset>("ContentsData/Contents1").text;
            mContentsData = JsonUtility.FromJson<ContentsData>(json);
            mQnATable = TableFactory.LoadContents1Table();
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
