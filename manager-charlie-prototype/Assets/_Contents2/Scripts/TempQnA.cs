using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;


namespace Content2
{
    public class TempQnA : MonoBehaviour
    {
        public void AnswerBtn()
        {
            CDebug.Log("Answer");
        }

        public void WanswerBtn()
        {
            CDebug.Log("Oops, Try Again");
        }
    }
}