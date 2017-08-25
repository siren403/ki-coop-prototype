using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;
using Contents;


namespace Contents1
{
    public class UIContents1 : MonoBehaviour, IQnAContentsUI
    {
        private SceneContents1 mScene = null;

        public GameObject InstPanelEpisode = null;
        public List<Button> InstBtnEpisodeList = null;

        public GameObject InstPanelAnswer = null;
        public List<Button> InstBtnAnswerList = null;

        public void Initialize(SceneContents1 scene)
        {
            mScene = scene;

            InstBtnEpisodeList[0].onClick.AddListener(() => OnBtnSelectEpisode(0));
            InstBtnEpisodeList[1].onClick.AddListener(() => OnBtnSelectEpisode(1));
            InstBtnEpisodeList[2].onClick.AddListener(() => OnBtnSelectEpisode(2));
            InstBtnEpisodeList[3].onClick.AddListener(() => OnBtnSelectEpisode(3));

            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));
            InstBtnAnswerList[2].onClick.AddListener(() => OnBtnSelectAnswer(2));
            InstBtnAnswerList[3].onClick.AddListener(() => OnBtnSelectAnswer(3));
        }



        public void OnBtnSelectEpisode(int episodeID)
        {
            mScene.SelectEpisode(episodeID);
            InstPanelEpisode.SetActive(false);
        }

        public void OnBtnSelectAnswer(int selection)
        {
            mScene.SelectAnswer(selection);
            //* 2. 선택한 번호 전달 */
            //* 2. SceneContents1.cs  __ 함수 호출*/
            InstPanelAnswer.SetActive(false);
        }


        public void ShowEpisode()
        {
            InstPanelEpisode.SetActive(true);
        }
        public void ShowSituation()
        {
            CDebug.Log("Play Animation");
        }
        public void ShowQuestion()
        {
            CDebug.Log("Play Question");
        }
        public void ShowAnswer()
        {
            InstPanelAnswer.SetActive(true);
            //* 1. 정답 데이터 설정*/
           
        }
        public void SelectAnswer()
        {
            mScene.RewardConfirm();
        }
        public void Evaluation(int answer)
        {

        }
        public void ShowReward()
        {
            mScene.ChangeState(QnAContentsBase.State.Clear);
        }
        public void ClearEpisode()
        {

        }



        
    }

}