using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using CustomDebug;

namespace Content2
{
    public class Qexposure : MonoBehaviour
    {
        
        public Transform Target;                    // 문제 제출 대상
        public GameObject NamePanel;                // 문제 제출 대상의 이름 Text의 부모 GameObject
        public Text NameText;                       // 문제 제출 대상의  이름 Text
        public GameObject ButtonPanel;              // 문제 제출 Animation이 완료 되었을 경우 클릭을 할 수 있게 해주는 버튼의 부모 GameObject

        void Start()
        {
            StartCoroutine("SeqShowName");
        }

        IEnumerator SeqShowName()
        {
            Vector3 Xy;     // 문제 제출 대상의 Size 확대 Vector 값 설정
            Xy.x = 2;
            Xy.y = 2;
            Xy.z = 1;

            NamePanel.SetActive(false);

            // 이미지 리소스가 도착하면 이미지 리소스로 교체..해야하는데..
            Sequence MySequence = DOTween.Sequence();
            MySequence.Append(Target.DOScale(Xy, 1));
            MySequence.Play();

            yield return new WaitForSeconds(1.5f);
            NamePanel.SetActive(true);
            // 기획서가 도착하면 NameText를 문제로 바꿔줘야할 자리

            yield return new WaitForSeconds(1.5f);
            CDebug.Log("Next Scene Set Ready");
            ButtonPanel.SetActive(true);
        }

        public void SceneSubmissionBtn()
        {
            SceneManager.LoadScene("SceneSubmission");
        }
    }
}
