using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents;
using System;
using LitJson;
using CustomDebug;

namespace Contents2
{
    public class UIContents2 : MonoBehaviour, IQnAContentsUI
    {
        [SerializeField]
        private GameObject InstPanelEpisode = null;
        [SerializeField]
        private List<Button> InstBtnEpisodeList = new List<Button>();
        [SerializeField]
        private GameObject InstPanelAnswer = null;
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();

        private SceneContents2 mScene = null;

        private string[] mAnswerData = null;

        public void Initialize(SceneContents2 scene)
        {
            mScene = scene;

            InstBtnEpisodeList[0].onClick.AddListener(() => SelectEpisodeEvent(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => SelectEpisodeEvent(1));
            InstBtnEpisodeList[2].onClick.AddListener(() => SelectEpisodeEvent(2));
            InstBtnEpisodeList[3].onClick.AddListener(() => SelectEpisodeEvent(3));

            InstBtnAnswerList[0].onClick.AddListener(() => EvaluationEvent(0));
            InstBtnAnswerList[1].onClick.AddListener(() => EvaluationEvent(1));
        }
        private void SelectEpisodeEvent(int episodeID)
        {
            mScene.StartEpisode(episodeID);
            InstPanelEpisode.SetActive(false);
        }

        private void EvaluationEvent(int answerID)
        {
            bool result = mScene.Evaluation(answerID);

            if(result)
            {
                mScene.ChangeState(QnAContentsBase.State.Reward);
            }
            else
            {
                mScene.ChangeState(QnAContentsBase.State.Question);
            }
            InstPanelAnswer.SetActive(false);
        }
        public void ShowEpisode()
        {
            InstPanelEpisode.SetActive(true);
        }

        public void ShowSituation()
        {
            CDebug.Log("Situation Data Set");
        }

        public void ShowQuestion()
        {
            CDebug.LogFormat("Please, Recylces throw out {0},{0},{0}", mScene.GetRecylces());
        }

        public void ShowAnswer()
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animaition... \n Input Close");
            mAnswerData = mScene.GetAnswersData();
            foreach(var answer in mAnswerData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
            InstPanelAnswer.SetActive(true);
        }

        public void SelectAnswer()
        {
            CDebug.Log("Wait Show Answer Animaition... \n InPut Open");
        }

        public void ShowReward()
        {
            CDebug.Log("Play Reward Animation");
            mScene.ChangeState(QnAContentsBase.State.Clear);
        }

        public void ClearEpisode()
        {
            CDebug.Log("Play Clear Animation");
            CDebug.Log("Stop Clear Animation by Show Outro");
        }

    }
}
