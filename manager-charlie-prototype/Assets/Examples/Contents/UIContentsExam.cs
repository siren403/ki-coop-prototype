using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using UIComponent;
using Util;

namespace Examples
{
    public class UIContentsExam : MonoBehaviour, IQnAView, IViewInitialize
    {

        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        [SerializeField]
        private GameObject InstPanelAnswer = null;
        [SerializeField]
        private List<Button> InstBtnAnswerList = new List<Button>();
        private SceneContentsExam mScene = null;
        private string[] mAnswersData = null;

        public void Initialize(QnAContentsBase scene)
        {
            mScene = scene as SceneContentsExam;

            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.Initialize(i, OnBtnSelectEpisodeEvent);
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            //선택지 버튼
            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));
            InstBtnAnswerList[2].onClick.AddListener(() => OnBtnSelectAnswer(2));
            InstBtnAnswerList[3].onClick.AddListener(() => OnBtnSelectAnswer(3));
        }

        #region 유저 입력 시 이벤트 처리 함수(ex. animation, sound play...)
        private void OnBtnSelectEpisodeEvent(int episodeID)
        {
            Debug.Log(episodeID);
            mScene.StartEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }
        private void OnBtnSelectAnswer(int answerID)
        {
            mScene.SelectAnswer(answerID);
            
        }
        #endregion


        public void ShowEpisode()
        {
            //InstPanelEpisode.SetActive(true);
        }
        public void ShowSituation()
        {
            CDebug.Log(string.Format("Playing Situation... Duration {0} sec", 1.5f));
        }
        public void ShowQuestion()
        {
            CDebug.LogFormat("Please, give me food starting with {0},{0},{0}", mScene.CurrentPhonics);
        }
        public void ShowAnswer()
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animation... \n Input Close");
            mAnswersData = mScene.GetAnswersData();
            foreach(var answer in mAnswersData)
            {
                CDebug.LogFormat("Answer : {0}", answer);
            }
            InstPanelAnswer.SetActive(true);
        }
        public void SelectAnswer()
        {
            CDebug.Log("Wait Show Answers Animation... \n Input Open");
        }
        public void CorrectAnswer()
        {
            CDebug.Log("Correct!! with animation");
        }
        public void WrongAnswer()
        {
            CDebug.Log("Wrong... with animation");
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
