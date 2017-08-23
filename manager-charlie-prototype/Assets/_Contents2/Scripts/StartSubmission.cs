using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CustomDebug;
using UnityEngine.SceneManagement;

namespace Contents2
{
    public class StartSubmission : MonoBehaviour
    {

        public GameObject Joy;              // Save the Earth 주인공 Joy

        public GameObject Mission;          // 문제 출장 대상 

        public Image ChangeBackground;      // 에피소드 이미지가 들어갈 공간

        // Episode Background Image Prefab
        public Image Episode1;
        public Image Episode2;
        public Image Episode3;
        public Image Episode4;
        public Image Episode5;

        public float WaitSec = 3.0f;        // 대기 시간

        void Awake()
        {
            // 추후 에피소드별로 배경화면 변경 예정
           //ChangeBackground.sprite = Episode1.sprite;
        }

        void Start()
        {
            CDebug.Log("Joy Animation Start");
            StartCoroutine("StartAni");
        }

        IEnumerator StartAni()
        {
            yield return new WaitForSeconds(WaitSec);

            Sequence MySequence = DOTween.Sequence();
            MySequence.Append(Mission.transform.DOMoveX(350,1));
            MySequence.Play();

            yield return new WaitForSeconds(WaitSec);
            CDebug.Log("분리 수거 할꺼 발견!");

            WaitSec = 5.0f;
            yield return new WaitForSeconds(WaitSec);
            SceneManager.LoadScene("02.SceneQexposure");
        }
    }
}