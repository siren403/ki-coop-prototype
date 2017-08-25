using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents;
using System;
using CustomDebug;
using LitJson;
using Util.Inspector;


namespace Contents2
{
    public class SceneContents2 : QnAContentsBase
    {

        private const int CONTENTS_ID = 2;

        [SerializeField]
        private UIContents2 mInstUI = null;

        public override IQnAContentsUI UI
        {
            get
            {
                return mInstUI;
            }
        }

        private string[] mRecycles = new string[] { "toothbrush", "kettle", "magazine", "vase" };
        private int mCurrentRecycles = 0;
        private int mQuestionCount = 0;

        protected override void Initialize()
        {
            mInstUI.Initialize(this);
            ChangeState(State.Episode);
        }

        protected override QnAFiniteState CreateShowAnswer()
        {
            return new FSContents2ShowAnswer();
        }
        protected override QnAFiniteState CreateShowClearEpisode()
        {
            return new FSContents2ClearEpisode();
        }
        protected override QnAFiniteState CreateShowEpisode()
        {
            return new FSContents2ShowEpisode();
        }
        protected override QnAFiniteState CreateShowEvaluateAnswer()
        {
            return new FSContents2EvaluateAnswer();
        }
        protected override QnAFiniteState CreateShowQuestion()
        {
            return new FSContents2ShowQuestion();
        }
        protected override QnAFiniteState CreateShowReward()
        {
            return new FSContents2ShowReward();
        }
        protected override QnAFiniteState CreateShowSelectAnswer()
        {
            return new FSContents2SelectAnswer();
        }
        protected override QnAFiniteState CreateShowSituation()
        {
            return new FSContents2ShowSituation();
        }

        public void StartEpisode(int expisodeID)
        {
            ChangeState(State.Situation);
        }

        public string GetRecylces()
        {
            return mRecycles[mCurrentRecycles];
        }

        public string[] GetAnswersData()
        {
            string[] answers = new string[4]
            {
                "Plastic", "Metal", "Paper", "Glass"
            };
            return answers;
        }

        public bool Evaluation(int answerID)
        {
            if(answerID == 0)
            {
                return true;
            }
            return false;
        }

    }

    public class QnAContents2
    {
        public char Question;
        public string Answer;
        public string[] Wrongs;
    }


}
