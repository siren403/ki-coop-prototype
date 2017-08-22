using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Content2
{
    public class Qexposure : MonoBehaviour
    {
        public Transform Target;                    // 문제 제출 대상
        public GameObject NamePanel;                // 문제 제출 대상의 이름 Text의 부모 GameObject
        public Text NameText;                       // 문제 제출 대상의  이름 Text
        public GameObject ButtonPanel;              // 문제 제출이 다되었을 경우 클릭을 할 수 있게 해주는 버튼의 부모  GameObject

        void Start()
        {
            StartCoroutine("SeqShowName");
        }

        IEnumerator SeqShowName()
        {
            Vector3 XY;
            XY.x = 2;
            XY.y = 2;
            XY.z = 1;

            NamePanel.SetActive(false);

            // 이미지 리소스가 도착하면 이미지 리소스로 교체..해야하는데..
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(Target.DOScale(XY, 1));
            mySequence.Play();

            yield return new WaitForSeconds(1.5f);
            NamePanel.SetActive(true);
            // 기획서가 도착하면 NameText를 문제로 바꿔줘야할 자리

            yield return new WaitForSeconds(1.5f);
            Debug.Log("Next Scene Set Ready");
            ButtonPanel.SetActive(true);
        }

        public void Buttonclick()
        {
            SceneManager.LoadScene("C2_Submission");
        }
    }
}
