using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;

namespace Content2
{
    public class Submission : MonoBehaviour
    {

        public Transform backGround;        // 백 그라운드를 움직일 수 있게 해주는 대상
        public GameObject question;         // 문제 대상
        public Transform question1;         // 문제 1의 자리
        public Transform question2;         // 문제 2의 자리
        public GameObject answer;           // 정답 프리팹, Transform으로 추후에 변경
        public GameObject wAnswer;          // 오답 프리팹, Transform으로 추후에 변경


        void Start()
        {
            QuestionData();                               // QustionData -> QustionSubmit -> StartCoroutine("SeqShowSubmission")
        }

        void QuestionData()
        {
            // question,answer,wAnswer의 데이터를 바꿔준다.
            CDebug.Log("Data Set Complete.");
            QuestionSubmit();
        }

        void QuestionSubmit()
        {
            int randomSubmit = Random.Range(0, 2);      // 문제의 답을 랜덤으로 섞어준다.

            if (randomSubmit == 0)
            {
                // 나중에 문제에 대한 데이터가 나오면 Transform으로 위치 값만 바꿔준다.
                Instantiate(answer, question1);
                Instantiate(wAnswer, question2);
            }
            else
            {
                Instantiate(answer, question2);
                Instantiate(wAnswer, question1);
            }

            StartCoroutine("SeqShowSubmission");
        }

        IEnumerator SeqShowSubmission()
        {
            // 애니메이션 리소스가 오면 Debug를 빼고 애니메이션을 삽입할 자리
            CDebug.Log("Animation Start");

            yield return new WaitForSeconds(1.0f);
            CDebug.Log("Animation End");

            yield return new WaitForSeconds(1.0f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(backGround.DOMoveY(400, 1));
            mySequence.Play();
        }
    }
}