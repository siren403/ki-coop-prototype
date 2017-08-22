using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Content2
{
    public class Submission : MonoBehaviour
    {

        public Transform BackGround;        // 백 그라운드를 움직일 수 있게 해주는 대상
        public Transform Question1;         // 문제 1의 자리
        public Transform Question2;         // 문제 2의 자리
        public GameObject Red;               // 문제 1번 프리팹
        public GameObject Blue;              // 문제 2번 프리팹

        void Awake()
        {
            Instantiate(Red, Question1);        // 문제 생성 랜덤으로 바꾼다.
            Instantiate(Blue, Question2);       
        }

        void Start()
        {
            StartCoroutine("SeqShowSubmission");
        }

        IEnumerator SeqShowSubmission()
        {
            // 애니메이션 리소스가 오면 Debug를 빼고 애니메이션을 삽입할 자리
            Debug.Log("Animation Start");

            yield return new WaitForSeconds(1.0f);
            Debug.Log("Animation End");

            yield return new WaitForSeconds(1.0f);
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(BackGround.DOMoveY(400, 1));
            mySequence.Play();
        }
    }
}