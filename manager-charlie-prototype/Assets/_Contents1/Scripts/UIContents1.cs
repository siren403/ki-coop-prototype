using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;
using Contents.QnA;


namespace Contents1
{
    public class UIContents1 : MonoBehaviour, IQnAContentsView
    {
        private SceneContents1 mScene = null;

        public GameObject InstPanelEpisode = null;
        public List<Button> InstBtnEpisodeList = null;

        public GameObject InstPanelAnswer = null;
        public List<Button> InstBtnAnswerList = null;

        public GameObject InstPanelGuage = null;
        public Slider InstGuageBar = null;

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

        /**
         * @fn  public void OnBtnSelectAnswer(int selection)
         *
         * @brief   버튼을 선택한 답을 SceneContent1 클래스의 멤버 함수에 전달해주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         *
         * @param   selection   The selection.
         */

        public void OnBtnSelectAnswer(int selection)
        {
            mScene.SelectAnswer(selection);
            CDebug.Log(selection);
            //* 2. 선택한 번호 전달 */
            //* 2. SceneContents1.cs  __ 함수 호출*/
            InstPanelAnswer.SetActive(false);
        }

        /**
         * @fn  public void ShowEpisode()
         *
         * @brief   에피소드 선택화면에서 각 패널을 On/Off 시켜주는 함수
         *          관련 패널 - InstPanelGuage, InstPanelEpisode 
         *
         * @author  Byeong
         * @date    2017-08-25
         */

        public void ShowEpisode()
        {
            InstPanelGuage.SetActive(false);
            InstPanelEpisode.SetActive(true);
        }

        /**
         * @fn  public void ShowSituation()
         *
         * @brief   상황 애니메이션을 보여주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         */

        public void ShowSituation()
        {
            InstPanelGuage.SetActive(true);
            CDebug.Log("Play Animation");
        }

        /**
         * @fn  public void ShowQuestion()
         *
         * @brief   질문 애니메이션을 보여주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         */

        public void ShowQuestion()
        {
            CDebug.Log("Play Question");
            var answers = mScene.GetAnswers();
            InstPanelAnswer.SetActive(true);
            for (int i = 0; i < InstBtnAnswerList.Count; i++)
            {
                InstBtnAnswerList[i].GetComponentInChildren<Text>().text = answers[i];
            }

            mScene.ChangeState(QnAContentsBase.State.Select);
        }

        public void ShowAnswer()
        {
            InstPanelAnswer.SetActive(true);
            //* 1. 정답 데이터 설정*/
           
        }

        public void SelectAnswer()
        {

        }

        public void Evaluation(int answer)
        {

        }

        public void ShowReward()
        {
            CDebug.Log("You Get Reward!!!!");
            mScene.ChangeState(QnAContentsBase.State.Clear);
        }
        public void ClearEpisode()
        {

        }

        public void CorrectAnswer()
        {
            InstGuageBar.value = mScene.Contents1GuagePercent;
        }

        public void WrongAnswer()
        {
            
        }
    }

}