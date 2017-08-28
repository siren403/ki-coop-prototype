using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Contents.QnA;
using System;
using CustomDebug;
using LitJson;
using Util.Inspector;
using Contents.Data;

namespace Contents2
{
    public class Qna
    {
        public int QuestionId;
        public int EpisodeId;

        public string Question;
        public string Correct;
        public string Wrong;
        public string ObjectState;



        /**id = 문제 id ,  foodID = 음식 id , name = 음식 이름 , used  = 이전에 제출 되었던 문제 이면 true로 변경해준다 */

        /**전체적 흐름 */
        /** 1. 문제 설정 : Qna 에서 랜덤으로 하나 뽑음 -> used 가 true 인것을 제외하고 문제 한개를 뽑는다. */
        /** 2. 정답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true로 변경하여 이전에 제출되었다는 것을 체크 해준다. */
        /** 3. 오답 설정  : Qna 에서 랜덤으로 하나 뽑음 -> used 를 true가 아니고 현재 문제 코드 (A=1, B=2, C=3 ,...)가 아닌 것들 세개 설정 -> */

        public Qna(int id, int episode, string question, string correct, string wrong, string objectstate)
        {
            QuestionId = id;
            EpisodeId = episode;
            Question = question;
            Correct = correct;
            Wrong = wrong;
            ObjectState = objectstate;
        }
    }


    public class SceneContents2 : QnAContentsBase
    {
        public List<Qna> QnaList = new List<Qna>();
        private const int CONTENTS_ID = 2;

        [SerializeField]
        private UIContents2 mInstUI = null;

   
        public override IQnAView UI
        {
            get
            {
                return mInstUI;
            }
        }

        private string[] mRecycles = new string[] { "toothbrush", "kettle", "magazine", "vase" };
        private int mCurrentRecycles = 0;
        private int mQuestionCount = 0;
        private JsonData mContentsData = null;
        private int mSetAnswerId =0;
        private int mSelectedAnswerID = 0;
        private int mRandomCorrectAnswerID = 0; //* 0,1,중 하나 선택 -> 0이면 왼쪽, 1이면 오른쪽에 배치 */
        private string mCorrectAnswer;
        private string mWrongAnswer;
        private string mQuestionObject;

        public int QuestionCount
        {
            get
            {
                return mQuestionCount;
            }
        }
        public string QuestionObject
        {
            get
            {
                return mQuestionObject;
            }
        }


        public string CorrectAnswer
        {
            get
            {
                return mCorrectAnswer;
            }
        }

        public string WrongAnswer
        {
            get
            {
                return mWrongAnswer;
            }
        }


        public int RandomCorrectAnswerID
        {
            get
            {
                return mRandomCorrectAnswerID;
            }
        }

        public int SelectedAnswerID
        {
            get
            {
                return mSelectedAnswerID;
            }
        }


        public int EpisodeCount
        {
            get
            {
                return mContentsData["episode"].Count;
            }
        }

        protected override void Initialize()
        {

            string json = Resources.Load<TextAsset>("ContentsData/Contents2").text;
            mContentsData = JsonMapper.ToObject(json);
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

        public void StartEpisode(int episodeID)
        {
            CDebug.Log("Episode Id " + episodeID + "이 선택되었습니다.");
            //* Episode id를 받아  옴*/

            //* 한 episode 별로 문제 가져옴 */
            if (episodeID == 1)
            {
                for (int i = 0 * 10; i < 10; i++)
                {
                    QnaList.Add(new Qna(TableFactory.LoadContents2Table().dataArray[i].ID, TableFactory.LoadContents2Table().dataArray[i].Episode, TableFactory.LoadContents2Table().dataArray[i].Question
                        , TableFactory.LoadContents2Table().dataArray[i].Correct, TableFactory.LoadContents2Table().dataArray[i].Wrong, TableFactory.LoadContents2Table().dataArray[i].Objectstate));
                }

            }
            else if (episodeID == 2 )
            {

            }
            ChangeState(State.Situation);
        }


        
        public void SelectAnswer(int id)
        {
            mSelectedAnswerID = id;
            ChangeState(State.Evaluation);
        }


        //*질문을 설정하고 정답 오른쪽 왼쪽에 random 으로 배치한다 */ 
        public void SetQuestion()
        {
            mQuestionCount = mQuestionCount + 1;
            mSetAnswerId = UnityEngine.Random.Range(0, QnaList.Count);

            for (int i = 0; i < QnaList.Count; i++)
            {
                CDebug.Log(i + "번째 item  :" + QnaList[i].Question);
            }

            CDebug.Log(mSetAnswerId + " 번째 QnA ID 설정됨");
            mCorrectAnswer = QnaList[mSetAnswerId].Correct;
            mWrongAnswer = QnaList[mSetAnswerId].Wrong;
            mQuestionObject = QnaList[mSetAnswerId].Question;

            mRandomCorrectAnswerID = UnityEngine.Random.Range(0,2);
            if (mRandomCorrectAnswerID == 0)
            {
                CDebug.Log("정답 왼쪽에 배치");
            }
            else if (mRandomCorrectAnswerID == 1)
            {
                CDebug.Log("정답 오른쪽에 배치");
            }

   
        }

        //*이전에 사용한 데이터 지워준다*/
        public void EraseData()
        {
            CDebug.Log(mSetAnswerId + " 번째 data 지워줌");
            QnaList.RemoveAt(mSetAnswerId);
        
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
