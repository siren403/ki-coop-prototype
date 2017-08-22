using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

namespace Content2
{
    public class TempStart : MonoBehaviour
    {
        public GameObject Submit;   // 문제 제출 이미지
        public float TmpAni = 1.0f;

        void Start()
        {
            StartCoroutine("SubmissionStart");
        }

        IEnumerator SubmissionStart()
        {
            yield return new WaitForSeconds(TmpAni);
        }
    }
}