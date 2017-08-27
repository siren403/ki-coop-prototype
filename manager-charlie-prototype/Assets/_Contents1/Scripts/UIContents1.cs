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
    public class UIContents1 : MonoBehaviour, IQnAView
    {
        private SceneContents1 mScene = null;

        public GameObject InstPanelEpisode = null;
        public List<Button> InstBtnEpisodeList = null;

        public GameObject InstPanelAnswer = null;
        public List<Button> InstBtnAnswerList = null;

        public List<GameObject> InstImgBlockList = null;

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
            CDebug.Log(selection);
            mScene.SelectAnswer(selection);
            //* 2. 선택한 번호 전달 */
            //* 2. SceneContents1.cs  __ 함수 호출*/
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

            mScene.AnswerSetting();

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

            mScene.ChangeState(QnAContentsBase.State.Select);
        }

        // As Select
        public void ShowAnswer()
        {
            // 선택지 블럭 이미지 ON/OFF
            for (int i = 0; i < 4; i++)
            {
                bool info = mScene.BlockInfo[i];
                InstImgBlockList[i].SetActive(info);
            }

            CDebug.Log("-------Active Show Answer-----------");
            var answers = mScene.GetAnswers();

            InstPanelAnswer.SetActive(true);

            for (int i = 0; i < InstBtnAnswerList.Count; i++)
            {
                InstBtnAnswerList[i].GetComponentInChildren<Text>().text = answers[i];
            }

            //InstPanelAnswer.SetActive(true);
            //* 1. 정답 데이터 설정*/           
        }

        public void SelectAnswer()
        {
            InstPanelAnswer.SetActive(false);
            CDebug.Log("---- Answer Panel Off -----");
            mScene.ChangeState(QnAContentsBase.State.Evaluation);
        }

        public void Evaluation(int answer)
        {

        }

        public void ShowReward()
        {
            // Reward 게이지 체크
            if (mScene.ThisProblemCount < 2)
            {
                mScene.CorrectAnswerCount++;
                mScene.Contents1GuagePercent++;

                InstGuageBar.value = mScene.Contents1GuagePercent;
            }

            mScene.CurrentQuestionIndex++;
            mScene.ThisProblemCount = 0;

            mScene.BlockInfo[0] = false;
            mScene.BlockInfo[1] = false;
            mScene.BlockInfo[2] = false;
            mScene.BlockInfo[3] = false;

            if(mScene.CurrentQuestionIndex >= 10)
            {
                CDebug.Log("Again answer");
                mScene.ChangeState(QnAContentsBase.State.Clear);
            }
            else
            {
                CDebug.Log("Done answer");
                mScene.ChangeState(QnAContentsBase.State.Situation);
            }
        }
        public void ClearEpisode()
        {
            CDebug.Log("Clear Episode!");
            mScene.ChangeState(QnAContentsBase.State.Episode);
        }

        // 맞췄을 경우 - as FSContents1Evaluation
        public void CorrectAnswer()
        {
            Debug.Log("UI CorrectAnswer");
            InstGuageBar.value = mScene.Contents1GuagePercent;

            mScene.ChangeState(QnAContentsBase.State.Reward);
        }

        // 못 맞췄을 경우 - as FSContents1Evaluation
        public void WrongAnswer()
        {
            Debug.Log("UI WrondAnswer");

            // 오답 개수가 3개 미만 일 경우
            if (mScene.ThisProblemCount < 2)
            {
                mScene.ThisProblemCount++;
                mScene.BlockInfo[mScene.Contents1AnswerNumber] = true;
                //mScene.ChangeState(QnAContentsBase.State.Situation);
                mScene.ChangeState(QnAContentsBase.State.Select);
            }
            // 오답 개수가 3개 이상일 경우, 정답 처리 후 넘어감
            else
            {
                CDebug.Log("Fail Answer");
                CorrectAnswer();
            }
        }
    }
}