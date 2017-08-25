using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;

namespace Contents
{
    public class UIContents1 : MonoBehaviour, IQnAContentsUI
    {
        [SerializeField]
        private GameObject InstPanelEpisode = null;
        [SerializeField]
        private List<Button> InstBtnEpisodeList = new List<Button>();
        [SerializeField]
        private GameObject InstPanelAnswer = null;
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();

        private string[] mAnswersData = null;

        private SceneContents1 mScene = null;

        public void Initialize(SceneContents1 scene)
        {
            mScene = scene;
            //에피소드 버튼
            InstBtnEpisodeList[0].onClick.AddListener(() => SelectEpisodeEvent(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => SelectEpisodeEvent(1));
            InstBtnEpisodeList[2].onClick.AddListener(() => SelectEpisodeEvent(2));
            InstBtnEpisodeList[3].onClick.AddListener(() => SelectEpisodeEvent(3));

            //선택지 버튼
            InstBtnAnswerList[0].onClick.AddListener(() => EvaluationEvent(0));
            InstBtnAnswerList[1].onClick.AddListener(() => EvaluationEvent(1));
            InstBtnAnswerList[2].onClick.AddListener(() => EvaluationEvent(2));
            InstBtnAnswerList[3].onClick.AddListener(() => EvaluationEvent(3));
        }

        #region 유저 입력 시 이벤트 처리 함수(ex. animation, sound play...)
        private void SelectEpisodeEvent(int episodeID)
        {
            mScene.StartEpisode(episodeID);
            InstPanelEpisode.SetActive(false);
        }
        private void EvaluationEvent(int answerID)
        {
            bool result = mScene.Evaluation(answerID);
            if (result)
            {
                CDebug.Log("Correct!! with animation");
                mScene.ChangeState(QnAContentsBase.State.Reward);
            }
            else
            {
                CDebug.Log("Wrong... with animation");
                mScene.ChangeState(QnAContentsBase.State.Question);
            }
            InstPanelAnswer.SetActive(false);
        }
        #endregion


        public void ShowEpisode()
        {
            InstPanelEpisode.SetActive(true);
        }
        public void ShowSituation()
        {
            CDebug.Log(string.Format("Playing Situation... Duration {0} sec", 1.5f));
        }
        public void ShowQuestion()
        {

            CDebug.LogFormat("Please, give me food starting with {0},{0},{0}", mScene.GetPhonics());
        }
        public void ShowAnswer()
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animation... \n Input Close");
            mAnswersData = mScene.GetAnswersData();
            foreach (var answer in mAnswersData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
            InstPanelAnswer.SetActive(true);
        }
        public void SelectAnswer()
        {
            CDebug.Log("Wait Show Answers Animation... \n Input Open");
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