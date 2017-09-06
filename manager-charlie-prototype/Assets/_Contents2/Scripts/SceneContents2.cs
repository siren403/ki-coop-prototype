using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using Contents.QnA;
using FSM;
using System;
using Util.Inspector;
using LitJson;
using System.Linq;
using Util;

namespace Contents2
{
    public class SceneContents2 : QnAContentsBase
    {
        /** 콘텐츠 관련 멤버 */
        private int mSelectedEpisode = 0;       // 유저가 위치하고 있는 에피소드를 체크하는 변수        
        public bool HasNextEpisode
        {
            get
            {
                return mSelectedEpisode < EpisodeCount;
            }
        }
        public List<QnaContents2Data> QnAList = new List<QnaContents2Data>();

        [SerializeField]
        private ViewContents2 mInstUI = null;

        /** @brief 리소스 외부 Attach */
        [SerializeField]
        private QuickSheet.Contents2 mQnATable = null;

        public override IQnAView View
        {
            get
            {
                return mInstUI;
            }
        }

        #region QnAContents를 구성하는 기본적인 State객체
        protected override QnAFiniteState FSEpisode { get { return new FSContents2ShowEpisode(); } }
        protected override QnAFiniteState FSSituation { get { return new FSContents2ShowSituation(); } }
        protected override QnAFiniteState FSQuestion { get { return new FSContents2ShowQuestion(); } }
        protected override QnAFiniteState FSAnswer { get { return new FSContents2ShowAnswer(); } }
        protected override QnAFiniteState FSSelect { get { return new FSContents2SelectAnswer(); } }
        protected override QnAFiniteState FSEvaluate { get { return new FSContents2EvaluateAnswer(); } }
        protected override QnAFiniteState FSReward { get { return new FSContents2ShowReward(); } }
        protected override QnAFiniteState FSClear { get { return new FSContents2ClearEpisode(); } }
        #endregion

        private int mQuestionCount = 0;
        private JsonData mContentsData = null;
        private int mSetAnswerId =0;
        private int mSelectedAnswerID = 0;
        private int mRandomCorrectAnswerID = 0; //* 0,1,중 하나 선택 -> 0이면 왼쪽, 1이면 오른쪽에 배치 */
        private string mCorrectAnswer;
        private string mWrongAnswer;
        private string mQuestionObject;
        private int mCurrentEpisode;

        private int mMaximumQuestion = 10;
        //I*SelectEpisode함수에서 초기화 해준다 */
        private int mCorrectCount;

        private Dictionary<string, Queue<QuickSheet.Contents2Data>> mQnA = null;

        public float CorrectProgress
        {
            get
            {
                return (float)mCorrectCount / mMaximumQuestion;
            }
        }

        //다음 문제가 있는지?
        public bool HasNextQuestion
        {
            get
            {
                return mCorrectCount < mMaximumQuestion;
            }
        }
        //* 현재 선택된 에피소드 */
        public int CurrentEpisode
        {
            get
            {
                return mCurrentEpisode;
            } 
        }
        //* 질문 Object 이름 */
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

        //*전체 에피소드 갯수 */
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
        
      

        public void SelectEpisode(int episodeID)
        {
            mCorrectCount = 0;
            //* Episode id를 받아  옴*/
            CDebug.Log("Episode Id " + episodeID + "이 선택되었습니다.");
            //* 현재 선택된 에피소드 값을 mCurrentEpisode 에 저장한다*/
            mCurrentEpisode = episodeID;
            //수정
            //* Episode id별로 data 받아온다 */
            mSelectedEpisode = episodeID;
            var table = mQnATable.dataArray
                                 .Where((data) => data.Episode == mSelectedEpisode)
                                 .ToList();

            //* QnaList에 있는 정보들 초기화*/
            QnAList.Clear();
            //* 받아온 데이터를 QnaList에 넣는다 */
            foreach (var row in table)
            {
                QnAList.Add(new QnaContents2Data(row.ID, row.Episode, row.Question, row.Correct, row.Wrong, row.Objectstate));
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
            mSetAnswerId = UnityEngine.Random.Range(0, QnAList.Count);

            for (int i = 0; i < QnAList.Count; i++)
            {
                CDebug.Log(i + "번째 item  :" + QnAList[i].Question);
            }

            CDebug.Log(mSetAnswerId + " 번째 QnA ID 설정됨");
            mCorrectAnswer = QnAList[mSetAnswerId].Correct;
            mWrongAnswer = QnAList[mSetAnswerId].Wrong;
            mQuestionObject = QnAList[mSetAnswerId].Question;

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

        //* Contents2 는 문제 맞춘수가 나온수와 같으니 같이 처리 해준다*/
        public void IncrementCorrectCount()
        {
            mCorrectCount++;
        }


        //*이전에 사용한 데이터 지워준다*/
        public void EraseData()
        {
            CDebug.Log(mSetAnswerId + " 번째 data 지워줌");
            QnAList.RemoveAt(mSetAnswerId);
        
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
