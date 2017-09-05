using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Contents.QnA;
using System;
using LitJson;
using CustomDebug;
using DG.Tweening;
using UIComponent;
using UniRx;

namespace Contents2
{
    public class ViewContents2 : MonoBehaviour, IQnAView, IViewInitialize
    {
        public GridSwipe InstPanelEpisodeList = null;
        public EpisodeButton PFEpisodeButton = null;

        public Sprite InstLockImage;

        public CorrectGauge InstCorrectGuage = null;

        //* 문제 Situation panel*/

        public GameObject InstPanelSituation = null;
        public Text InstTextSituation;

        public GameObject InstPanelQuestion = null;

        public GameObject InstPanelQuestionBack = null;

        public GameObject InstImgCharacter = null;

        public GameObject InstImgItem= null;

        //*가시적으로 보기위해 임시로 text 추가함  */
        public GameObject InstTextQuestion = null;
        public Text InstTextQuestionObject;

        //*정답 선택지 판넬*/
        public GameObject InstPanelAnswer = null;                            

        //* 정답 버튼*/
        public List<Button> InstBtnAnswerList = new List<Button>();


        //* 오답 선택 했을 때 가려주는 이미지*/
        public GameObject InstPanelLeftWrong = null;
        public GameObject InstPanelRightWrong = null;

        public GameObject InstPanelReward = null;

        public GameObject InstPanelClear = null;


        //* 10초이상 아무 선택 없을 시 나오는 Panel*/
        public GameObject InstPanelSelectAnswer = null;


        /** @brief Outro UI*/
        public MenuOutro InstOutro = null;

        /** @brief 컨텐츠2 Scene 클래스 */
        private SceneContents2 mScene = null;

        public string[] mAnswerData = null;                                        // 정답 데이터를 로드


        /** @brief Debuggin */
        public Button InstBtnDoubleSpeed = null;
        private int mDebugTimeScale = 1;


        public void Initialize(QnAContentsBase scene)
        {
            mScene = scene as SceneContents2;                                                         // 이 UI의 씬을 불러온다.

            for (int i = 0; i < mScene.EpisodeCount; i++)
            {
                var btn = Instantiate<EpisodeButton>(PFEpisodeButton, InstPanelEpisodeList.TargetGrid.transform);
                btn.ID = i + 1;
                btn.OnButtonUp = OnBtnSelectEpisodeEvent;
            }
            InstPanelEpisodeList.TargetGrid.Reposition();

            // 정답 선택 버튼 2개
            InstBtnAnswerList[0].onClick.AddListener(() => EvaluationEvent(0));
            InstBtnAnswerList[1].onClick.AddListener(() => EvaluationEvent(1));

            //Outro
            InstOutro.Initialize(
                onLoadMiniGame: () =>
                {
                    //todo : 미니게임 Scene 구현 후 전환 코드 추가 필요
                    OnClickOutroBtnEvent(1);
                },
                onRetryEpisode: () =>
                {
                    OnClickOutroBtnEvent(2);
                },
                onNextEpisode: () =>
                {
                    OnClickOutroBtnEvent(3);
                },
                isEnableNextEpisode: ()=> 
                {
                    return mScene.HasNextEpisode;
                });

            InstBtnDoubleSpeed.OnClickAsObservable()
                .Select(_ => mDebugTimeScale = (int)Mathf.Repeat(mDebugTimeScale + 1, 4))
                .Subscribe(timeScale =>
                {
                    Time.timeScale = timeScale;
                    InstBtnDoubleSpeed.GetComponentInChildren<Text>().text = string.Format("X{0}", timeScale);
                });
        }




        private void OnBtnSelectEpisodeEvent(int episodeID,IDButton sender)
        {
            Debug.Log(episodeID);
            mScene.SelectEpisode(episodeID);
            InstPanelEpisodeList.gameObject.SetActive(false);

        }


        private void EvaluationEvent(int answerID)
        {
            CDebug.Log( answerID +"번 선택");
            InstBtnAnswerList[answerID].transform.DOScale(Vector3.one * 1.4f, 0.5f).SetLoops(2, LoopType.Yoyo);
            mScene.SelectAnswer(answerID);

        }

        private void OnClickOutroBtnEvent(int buttonID)
        {
            //홈화면
            if (buttonID == 0)
            {
                CDebug.Log("Home");

            }
            //미니게임
            else if (buttonID == 1)
            {
                CDebug.Log("MiniGame");
            }
            //현재 에피소드 재시작
            else if (buttonID == 2)
            {
                CDebug.Log("ReStart");
                //* 게이지 바 초기화 한 후 다시 현재 에피소드 선택 함수를 호출 한다*/
                InitGaugeBar();
                mScene.SelectEpisode(mScene.CurrentEpisode);
            }
            //다음 에피소드
            else if (buttonID == 3)
            {
                CDebug.Log("Next Episode");
                //* 게이지 바 초기화 한 후 다음 에피소드 선택 함수를 호출 한다*/
                InitGaugeBar();
                if (mScene.CurrentEpisode < 5)
                {
                    mScene.SelectEpisode(mScene.CurrentEpisode + 1);
                }
                else
                {
                    CDebug.Log("다음 스테이지가 없습니다.");
                }
            }
        }

 
        public void ShowEpisode()                                                  
        {
            InstCorrectGuage.gameObject.SetActive(false);
            CDebug.Log("Contents2 - Episode 선택 화면 출력");
        }


        //* 문제 제출 애니메이션 출력 다 한 후에 FMS에서 시간을 체크해 상태를 Answer으로 넘긴다.*/
        public void ShowSituation()                                               
        {
            InstPanelQuestion.SetActive(false);
            InstPanelAnswer.SetActive(false);
            InstPanelLeftWrong.SetActive(false);
            InstPanelRightWrong.SetActive(false);
            InstPanelReward.SetActive(false);
            InstPanelClear.SetActive(false);
            InstOutro.Hide();

            InstPanelSituation.SetActive(true);
            InstCorrectGuage.gameObject.SetActive(true);

            StartCoroutine(SeqShowSituation());
        }
        IEnumerator SeqShowSituation()
        {
            InstTextSituation.text = "Hi I'm Joy !";
            CDebug.Log("Hi ~ I'm Joy ! ");
            yield return new WaitForSeconds(2.0f);
            InstTextSituation.text = "Oh! it's so messy ! ... ";
            CDebug.Log("Oh! it's so messy ! ... ");
            yield return new WaitForSeconds(2.0f);
            InstTextSituation.text = "By recycling we can..... Let's Recycle !.. ";
            CDebug.Log("몇초 뒤 캐릭터와 같이 출력 (파이팅 동작을 하며 Let's Recycle !) ");
        }



        public void ShowQuestion()
        {
            InstPanelSituation.SetActive(false);
            InstPanelAnswer.SetActive(false);
            InstPanelQuestion.SetActive(true);

            ////*뒷배경 , 아이템 사이즈 위치 원상복귀 */
            //InstPanelQuestionBack.transform.DOMoveX(InstPanelQuestionBack.transform.position.x + 10, 0);
            //InstImgItem.transform.DOScale(Vector3.one, 0);

       
            CDebug.Log("Situation Data Set -> situation 애니메이션 보여주면서 Question을 설정해준다");
            mScene.SetQuestion();

            InstTextQuestion.GetComponent<Text>().text = +((mScene.CorrectProgress * 10) + 1) + " 번 question : " + mScene.QuestionObject;
            InstTextQuestionObject.text = "" + mScene.QuestionObject;
            InstTextQuestion.SetActive(false);
            if (mScene.RandomCorrectAnswerID == 0)
            {
                InstBtnAnswerList[0].GetComponentInChildren<Text>().text = "정답" + mScene.CorrectAnswer;
                InstBtnAnswerList[1].GetComponentInChildren<Text>().text = "오답" + mScene.WrongAnswer;
            }
            else
            {
                InstBtnAnswerList[0].GetComponentInChildren<Text>().text = "오답" + mScene.WrongAnswer;
                InstBtnAnswerList[1].GetComponentInChildren<Text>().text = "정답" + mScene.CorrectAnswer;
            }

            StartCoroutine(SeqShowQuestion());
        }
        IEnumerator SeqShowQuestion()
        {
            yield return new WaitForSeconds(2.0f);
            InstPanelQuestionBack.transform.DOMoveX(InstPanelQuestionBack.transform.position.x - 10, 3);

            yield return new WaitForSeconds(3.0f);
            CDebug.Log("아이템 이름 출력 후 강조하면서 캐릭터 사라짐");
            InstImgItem.transform.DOScale(Vector2.one * 2.0f, 2);
            InstImgItem.transform.DOMove(Vector2.zero, 2);
            InstImgCharacter.transform.GetComponent<Image>().DOFade(0,1);

            yield return new WaitForSeconds(3.0f);
            CDebug.Log("Question의 이름이 출력되며 사운드 실행");
            InstImgItem.transform.DOScale(Vector2.one , 2);
            InstImgItem.transform.DOMove(new Vector2(1, 0), 2);
            InstTextQuestion.SetActive(true);
            InstImgCharacter.transform.GetComponent<Image>().DOFade(1, 1);


        }





        public void ShowAnswer()                                                    
        {
            CDebug.Log("Get Answers Data");
            CDebug.Log("Wait Show Answers Animaition... \n Input Close");
            InstPanelQuestion.SetActive(false);
            InstPanelAnswer.SetActive(true);

            //*Question 관련 UI 원위치 */
            InstImgItem.transform.localPosition = new Vector2(243, 0);
            InstPanelQuestionBack.transform.DOMoveX(InstPanelQuestionBack.transform.position.x + 10, 0);

            //* 선택지 출력 연출을 위해 사용*/
            StartCoroutine(SeqShowAnswer());
        }
        IEnumerator SeqShowAnswer()
        {
            //* 모든 선택지가 나오기 전에 버튼이 눌리지 않게 한다. */
            InstBtnAnswerList[0].enabled = false;
            InstBtnAnswerList[1].enabled = false;

            InstBtnAnswerList[0].gameObject.SetActive(false);
            InstBtnAnswerList[1].gameObject.SetActive(false);

            yield return new WaitForSeconds(1.0f);
            InstBtnAnswerList[0].gameObject.SetActive(true);

            yield return new WaitForSeconds(1.0f);
            InstBtnAnswerList[1].gameObject.SetActive(true);

            InstBtnAnswerList[0].enabled = true;
            InstBtnAnswerList[1].enabled = true;
        }




        public void HurryUpAnswer()
        {
            CDebug.Log("Wait Show Answer Animaition... \n InPut Open  -> Hurry Up! 넣어준다");

            InstPanelSelectAnswer.GetComponent<Image>().DOFade(1.0f, 1.0f).SetLoops(2, LoopType.Yoyo)
                   .OnStart(() =>
                   {
                       InstPanelSelectAnswer.gameObject.SetActive(true);
                   })
                   .SetDelay(1.5f)
                   .OnComplete(() =>
                   {
                       InstPanelSelectAnswer.gameObject.SetActive(false);
                   });
        }



        public void ShowReward()
        {
            CDebug.Log("Play Reward Animation -> 10문제 다 풀었을 경우, 팡파레 터지며 조이가 만세하며 기뻐한다.");
            //스티커 연출 후 ClearEpisode상태로 이행

            InstPanelReward.transform.DOScale(1.0f, 1.0f)
                   .OnStart(() =>
                   {
                       InstPanelReward.gameObject.SetActive(true);
                   })
                   .SetDelay(1.5f)
                   .OnComplete(() =>
                   {
                       mScene.ChangeState(QnAContentsBase.State.Clear);
                   });
        }

        public void ClearEpisode()
        {
            CDebug.Log("Play Clear Animation -> Save the Earth 메인 주제가가 나오고 조이가 춤춘다.");
            InstPanelReward.SetActive(false);
            InstPanelClear.SetActive(true);
            CDebug.Log("Stop Clear Animation by Show Outro");

            StartCoroutine(SeqClearEpisode());
        }
        IEnumerator SeqClearEpisode()
        {
            yield return new WaitForSeconds(1.0f);
            InstOutro.Show();
        }

        public void InitGaugeBar()
        {
            DOTween.To(() => InstCorrectGuage.Value, (x) => InstCorrectGuage.Value = x, 0, 0);
        }
        public void CorrectAnswer()
        {
            mScene.IncrementCorrectCount();

            //* 게이지가 찬다*/
            DOTween.To(() => InstCorrectGuage.Value, (x) => InstCorrectGuage.Value = x, mScene.CorrectProgress, 0.3f)
             .OnComplete(() =>
             {
                 //*나중에 수정해야 할 부분 ->오답시 아이템 가리는 효과*/
                 InstPanelLeftWrong.SetActive(false);
                 InstPanelRightWrong.SetActive(false);

                 if (mScene.HasNextQuestion)
                 {
                     CDebug.Log("Has Next Question -> 다음 문제로 넘어감");
                     mScene.ChangeState(QnAContentsBase.State.Question);
                 }
                 else
                 {
                     CDebug.Log("End Question -> Episode 문제를 다 맞춤");
                     mScene.ChangeState(QnAContentsBase.State.Reward);
                 }
             });
            CDebug.Log(" ///// " + mScene.HasNextQuestion + "   현재 진행 상황 : " +  mScene.CorrectProgress);
            //* 데이터 셋에서 지워줌*/
            mScene.EraseData();
            CDebug.Log(" 정답입니다. ");
        }

        //** mScene.RandomCorrectAnswerID 가 0 이면 InstPanelRightWrong 을 킴,  1 이면 InstPanelLeftWrong 을 켠다.*/
        public void WrongAnswer()
        {
            if (mScene.RandomCorrectAnswerID == 0)
            {
                InstPanelRightWrong.SetActive(true);
            }
            else
            {
                InstPanelLeftWrong.SetActive(true);
            }
            CDebug.Log(" 오답입니다. ");
        }

        public void ShowOutro()
        {
        }
    }
}
