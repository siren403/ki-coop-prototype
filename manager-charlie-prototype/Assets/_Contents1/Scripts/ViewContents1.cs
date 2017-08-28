using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;
using Contents.QnA;
using UIComponent;
using DG.Tweening;


namespace Contents1
{
    public class ViewContents1 : MonoBehaviour, IQnAView, IViewInitialize
    {
        private SceneContents1 mScene = null;

        #region create by seongho

        // 에피소드 버튼 동적 생성을 위해 기능성 컴포넌트로 교체
        // PFEpisodeButton에 링크시킨 Prefab은 구현을 위해 만든 임시 Prefab
        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;
        //public GameObject InstPanelEpisode = null;
        //public List<Button> InstBtnEpisodeList = null;

        public CorrectGuage InstCorrectGuage = null;
        public Image InstImgRewardSticker = null;
        public GameObject InstPanelClear = null;
        public GameObject InstOutro = null;

        private int mSelectedAnswerIndex = 0;

        //Outro 버튼
        public Button InstBtnHome = null;
        public Button InstBtnMiniGame = null;
        public Button InstBtnReplay = null;
        public Button InstBtnNext = null;

        #endregion
        public GameObject InstPanelAnswer = null;
        public List<Button> InstBtnAnswerList = null;

        public List<GameObject> InstImgBlockList = null;


        public void Initialize(QnAContentsBase scene)
        {
            //IViewInitialize 인터페이스 구현에 따라 형변환하여 참조
            mScene = scene as SceneContents1;

            // Scene에서 가져온 에피소드 개수를 사용하여 버튼 동적생성 및 ID 부여
            // Grid가 Attach되어있는 오브젝트 하위에 위치시킨 후 Reposition을 통해 정렬
            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.Initialize(i + 1, OnBtnSelectEpisodeEvent);
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));
            InstBtnAnswerList[2].onClick.AddListener(() => OnBtnSelectAnswer(2));
            InstBtnAnswerList[3].onClick.AddListener(() => OnBtnSelectAnswer(3));



            InstBtnHome.onClick.AddListener(() => CDebug.Log("Home"));
            InstBtnMiniGame.onClick.AddListener(() => CDebug.Log("MiniGame"));
            InstBtnReplay.onClick.AddListener(() => CDebug.Log("Replay"));
            InstBtnNext.onClick.AddListener(() => CDebug.Log("Next"));

        }


        public void OnBtnSelectEpisodeEvent(int episodeID)
        {
            mScene.SelectEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
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
            mSelectedAnswerIndex = selection;
            CDebug.Log(mSelectedAnswerIndex);
            mScene.SelectAnswer(mSelectedAnswerIndex);
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
            InstCorrectGuage.gameObject.SetActive(false);
            InstPanelEpisodeList.gameObject.SetActive(true);
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
            InstCorrectGuage.gameObject.SetActive(true);

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

            mScene.ChangeState(QnAContentsBase.State.Answer);
        }

        // As Select
        public void ShowAnswer()
        {
            CDebug.Log("View ShowAnswer");
            var answers = mScene.GetAnswers();
            for (int i = 0; i < answers.Length; i++)
            {
                InstBtnAnswerList[i].GetComponentInChildren<Text>().text = answers[i].Correct;
            }
            InstPanelAnswer.SetActive(true);

            // 선택지 블럭 이미지 ON/OFF
            for (int i = 0; i < mScene.BlockInfo.Length; i++)
            {
                bool info = mScene.BlockInfo[i];
                InstImgBlockList[i].SetActive(info);
            }

            mScene.ChangeState(QnAContentsBase.State.Select);
        }
        //현재는 사용할일이 없을거라 예상
        public void SelectAnswer()
        {
            
        }

        // 맞췄을 경우 - as FSContents1Evaluation
        public void CorrectAnswer()
        {
            Debug.Log("UI CorrectAnswer");
            // 블랙알파 후 캐릭터 애니메이션을 재생해야 하지만 현재는 리소스가 없으니
            // 선택지 패널 off
            InstPanelAnswer.gameObject.SetActive(false);
            DOTween.To(() => InstCorrectGuage.Value, (x) => InstCorrectGuage.Value = x, mScene.CorrectProgress, 0.3f)
                .OnComplete(()=> 
                {
                    if (mScene.HasNextQuestion)
                    {
                        CDebug.Log("Has Next Question");
                        mScene.ChangeState(QnAContentsBase.State.Answer);
                    }
                    else
                    {
                        mScene.ChangeState(QnAContentsBase.State.Reward);
                        CDebug.Log("End Question");
                    }
                });
            
        }

        // 못 맞췄을 경우 - as FSContents1Evaluation
        public void WrongAnswer()
        {
            Debug.Log("UI WrondAnswer");

            mScene.BlockInfo[mSelectedAnswerIndex] = true;
            InstImgBlockList[mSelectedAnswerIndex].SetActive(true);
            mScene.ChangeState(QnAContentsBase.State.Select);

            //// 오답 개수가 3개 미만 일 경우
            //if (mScene.ThisProblemCount < 2)
            //{
            //    mScene.ThisProblemCount++;
            //    mScene.BlockInfo[mScene.Contents1AnswerNumber] = true;
            //    //mScene.ChangeState(QnAContentsBase.State.Situation);
            //    mScene.ChangeState(QnAContentsBase.State.Select);
            //}
            //// 오답 개수가 3개 이상일 경우, 정답 처리 후 넘어감
            //else
            //{
            //    CDebug.Log("Fail Answer");
            //    CorrectAnswer();
            //}
        }
        public void ShowReward()
        {
            if(mScene.CorrectProgress == 1)//모든 문제 정답시의 값
            {
                //스티커 연출 후 ClearEpisode상태로 이행
                InstImgRewardSticker.transform.DOScale(1.0f, 1.0f)
                    .OnStart(()=> 
                    {
                        InstImgRewardSticker.gameObject.SetActive(true);
                    })
                    .SetDelay(1.5f)
                    .OnComplete(()=> 
                    {
                        mScene.ChangeState(QnAContentsBase.State.Clear);
                    });
            }
            else
            {
                //별다른 연출이 없다면 ClearEpisode상태로 이행
                mScene.ChangeState(QnAContentsBase.State.Clear);
            }
            

            //return;
            //// Reward 게이지 체크
            //if (mScene.ThisProblemCount < 2)
            //{
            //    mScene.CorrectAnswerCount++;
            //    mScene.Contents1GuagePercent++;

            //}

            //mScene.CurrentQuestionIndex++;
            //mScene.ThisProblemCount = 0;

            //mScene.BlockInfo[0] = false;
            //mScene.BlockInfo[1] = false;
            //mScene.BlockInfo[2] = false;
            //mScene.BlockInfo[3] = false;

            //if(mScene.CurrentQuestionIndex >= 10)
            //{
            //    CDebug.Log("Again answer");
            //    mScene.ChangeState(QnAContentsBase.State.Clear);
            //}
            //else
            //{
            //    CDebug.Log("Done answer");
            //    mScene.ChangeState(QnAContentsBase.State.Situation);
            //}
        }
        public void ClearEpisode()
        {
            CDebug.Log("Clear Episode!");
            InstImgRewardSticker.gameObject.SetActive(false);
            InstPanelClear.gameObject.SetActive(true);
            //클리어 시 애니메이션 재생
            // 터치 입력을 받으면 Outro UI 활성화
        }

        public void ShowOutro()
        {
            InstPanelClear.gameObject.SetActive(false);
            InstOutro.SetActive(true);
        }
    }
}