using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
using CustomDebug;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Contents.QnA;
using UIComponent;
using DG.Tweening;
using System.Linq;

namespace Contents1
{
    /**
     @class ViewContents1
    
     @brief 컨텐츠 1의 View
    
     @author    SEONG
     @date  2017-08-31
     */
    public class ViewContents1 : MonoBehaviour, IQnAView, IViewInitialize
    {
        private SceneContents1 mScene = null;

        // 에피소드 버튼 동적 생성을 위해 기능성 컴포넌트로 교체
        // PFEpisodeButton에 링크시킨 Prefab은 구현을 위해 만든 임시 Prefab
        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        public CorrectGauge InstCorrectGuage = null;
        public Image InstImgRewardSticker = null;
        public GameObject InstPanelClear = null;
        public MenuOutro InstOutro = null;

        private int mSelectedAnswerIndex = 0;

        private HashSet<int> mAnswerIndexSet = new HashSet<int>();

        /** @brief UI Input 제어를 위한 멤버 */
        public EventSystem InstEventSystem = null;
        /** @brief 선택지 패널 */
        public GameObject InstPanelAnswer = null;
        /** @brief 선택지 버튼 리스트 */
        public List<Button> InstBtnAnswerList = null;

        /**
         * View 초기화
         *
         * @author  SEONG
         * @date    2017-08-31
         *
         * @param   scene   The scene.
         */
        public void Initialize(QnAContentsBase scene)
        {
            //IViewInitialize 인터페이스 구현에 따라 형변환하여 참조
            mScene = scene as SceneContents1;

            // Scene에서 가져온 에피소드 개수를 사용하여 버튼 동적생성 및 ID 부여
            // Grid가 Attach되어있는 오브젝트 하위에 위치시킨 후 Reposition을 통해 정렬
            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.ID = i + 1;
                btn.OnButtonUp = OnBtnSelectEpisode;
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            InstBtnAnswerList[0].onClick.AddListener(() => OnBtnSelectAnswer(0));
            InstBtnAnswerList[1].onClick.AddListener(() => OnBtnSelectAnswer(1));
            InstBtnAnswerList[2].onClick.AddListener(() => OnBtnSelectAnswer(2));
            InstBtnAnswerList[3].onClick.AddListener(() => OnBtnSelectAnswer(3));

            InstOutro.Initialize(
                onLoadMiniGame: () => 
                {
                    CDebug.Log("Load MiniGame");
                },
                onRetryEpisode: ()=> 
                {
                    CDebug.Log("Retry");
                    InstOutro.Hide();
                    InstCorrectGuage.Value = 0;
                    mScene.RetryEpisode();
                },
                onNextEpisode: ()=> 
                {
                    InstOutro.Hide();
                    InstCorrectGuage.Value = 0;
                    mScene.NextEpisode();
                },
                isEnableNextEpisode: ()=> 
                {
                    return mScene.HasNextEpisode;
                });
        }
        /**
         * 전달한 버튼의 상태를 바꿈
         *
         * @author  SEONG
         * @date    2017-08-31
         *
         * @param   btn     The button control.
         * @param   enable  True to enable, false to disable.
         */
        private void ButtonChangeState(Button btn,bool enable)
        {
            if(enable)
            {
                btn.enabled = true;
                btn.image.color = Color.white;
            }
            else
            {
                btn.enabled = false;
                btn.image.color = new Color(0.6f, 0.6f, 0.6f, 0.5f);
            }          
        }        

        /**
         * Outro move
         * 다시 플레이하기
         *
         * @author  Seong
         * @date    2017-09-05
         *
         * @param   moveInfo    Information describing the move.
         */
        public void OutroMove(int moveInfo)
        {
            if(moveInfo == 3)
            {
                CDebug.Log("RePlay!");

                mScene.ChangeState(QnAContentsBase.State.Situation);
            }
        }
        /**
         * Shows the outro
         *
         * @author  Seong
         * @date    2017-09-05
         */
        public void ShowOutro()
        {
            InstPanelClear.gameObject.SetActive(false);
            InstOutro.Show();
        }
        /**
         * 에피소드 선택화면에서 각 패널을 On/Off 시켜주는 함수 관련 패널 - InstPanelGuage, InstPanelEpisode
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
         * 에피소드 버튼 선택 시 호출
         *
         * @author  SEONG
         * @date    2017-08-31
         *
         * @param   episodeID   Identifier for the episode.
         * @param   sender      The sender.
         */
        private void OnBtnSelectEpisode(int episodeID, IDButton sender)
        {
            mScene.SelectEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);
        }

        /**
         * 상황 애니메이션을 보여주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         */
        public void ShowSituation()
        {
            CDebug.Log("Play Situation");
            InstCorrectGuage.gameObject.SetActive(true);
        }
        /**
         * 질문 애니메이션을 보여주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         */
        public void ShowQuestion()
        {
            CDebug.Log("Play Question");

            mScene.ChangeState(QnAContentsBase.State.Answer);
        }
        /**
         * 선택지를 보여줌
         *
         * @author  SEONG
         * @date    2017-08-31
         */
        public void ShowAnswer()
        {
            CDebug.Log("View ShowAnswer");
            var answers = mScene.GetAnswers();
            for (int i = 0; i < answers.Length; i++)
            {
                InstBtnAnswerList[i].GetComponentInChildren<Text>().text = answers[i].Correct;
            }
            InstPanelAnswer.SetActive(true);

            mAnswerIndexSet.Clear();
            for (int i = 0; i < InstBtnAnswerList.Count; i++)
            {
                ButtonChangeState(InstBtnAnswerList[i], true);
                //선택지의 인덱스를 HashSet에 저장
                //후에 유저가 선택한 버튼의 인덱스를 꺼내서
                //3번 오답시 정답 선택지를 알아냄 
                mAnswerIndexSet.Add(i);
            }
            mScene.ChangeState(QnAContentsBase.State.Select);
        }

        /**
         * 버튼을 선택한 답을 SceneContent1 클래스의 멤버 함수에 전달해주는 함수
         *
         * @author  Byeong
         * @date    2017-08-25
         *
         * @param   answerIndex The selection.
         */
        public void OnBtnSelectAnswer(int answerIndex)
        {
            mSelectedAnswerIndex = answerIndex;
            CDebug.LogFormat("Select Answer Index : {0}",mSelectedAnswerIndex);
            //선택한 선택지 인덱스를 HashSet에서 제거
            if (mAnswerIndexSet.Contains(mSelectedAnswerIndex))
            {
                mAnswerIndexSet.Remove(mSelectedAnswerIndex);
            }

            mScene.SelectAnswer(mSelectedAnswerIndex);
        }

        //현재는 사용할일이 없을거라 예상
        public void HurryUpAnswer()
        {
            
        }

        /**
         * 정답 선택 시 정반응
         *
         * @author  SEONG
         * @date    2017-08-31
         */
        public void CorrectAnswer()
        {
            InstEventSystem.enabled = false;
            // 블랙알파 후 캐릭터 애니메이션을 재생해야 하지만 현재는 리소스가 없으니
            // 선택지 패널 off
            InstPanelAnswer.gameObject.SetActive(false);
            InstCorrectGuage.TweenValue(mScene.CorrectProgress,0.3f)
                .OnComplete(() =>
                {
                    if (mScene.HasNextQuestion)
                    {
                        CDebug.Log("Has Next Question");
                        mScene.ChangeState(QnAContentsBase.State.Question);
                    }
                    else
                    {
                        mScene.ChangeState(QnAContentsBase.State.Reward);
                        CDebug.Log("End Question");
                    }
                    InstEventSystem.enabled = true;
                });            
        }
        /**
         * 틀린 답 선택 시 오반응
         *
         * @author  SEONG
         * @date    2017-08-31
         */
        public void WrongAnswer()
        {
            ButtonChangeState(InstBtnAnswerList[mSelectedAnswerIndex], false);
            mScene.ChangeState(QnAContentsBase.State.Select);
        }
        /**
         * 3번 틀린 답 선택시 강제 진행
         *
         * @author  SEONG
         * @date    2017-08-31
         */
        public void PerfectWrongAnswer()
        {
            ButtonChangeState(InstBtnAnswerList[mSelectedAnswerIndex], false);

            //3번 틀린 이후라면 HashSet에는 하나의 인덱스만 남아있고
            //그 인덱스가 정답버튼의 인덱스
            CDebug.LogFormat("Correct Answer Index : {0}", mAnswerIndexSet.First());
            InstEventSystem.enabled = false;
            InstBtnAnswerList[mAnswerIndexSet.First()].image.DOColor(Color.green, 0.15f)
                .SetLoops(8, LoopType.Yoyo)
                .OnComplete(() =>
                {
                    if (mScene.HasNextQuestion)
                    {
                        CDebug.Log("Has Next Question");
                        InstEventSystem.enabled = true;
                        mScene.ChangeState(QnAContentsBase.State.Question);
                    }
                    else
                    {
                        mScene.ChangeState(QnAContentsBase.State.Reward);
                        CDebug.Log("End Question");
                    }
                    //mScene.ChangeState(QnAContentsBase.State.Question);
                });
        }

        /**
         * Shows the reward
         *
         * @author  Byeong
         * @date    2017-09-05
         */
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
        }

        /**
         * Clears the episode
         *
         * @author  Seong

         * @date    2017-09-05
         */
        public void ClearEpisode()
        {
            CDebug.Log("Clear Episode!");
            InstImgRewardSticker.gameObject.SetActive(false);
            InstPanelClear.gameObject.SetActive(true);
            //클리어 시 애니메이션 재생
            // 터치 입력을 받으면 Outro UI 활성화
        }

    }
}