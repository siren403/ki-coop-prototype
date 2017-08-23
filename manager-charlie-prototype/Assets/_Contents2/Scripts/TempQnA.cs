using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomDebug;


namespace Content2
{
    public class TempQnA : MonoBehaviour
    {

        public int WAnswerCount = 0; 

        public void AnswerBtn()
        {
            CDebug.Log("Answer");
        }

        public void WanswerBtn()
        {
            WAnswerCount++;
            SceneManager.LoadScene("04.SceneWAnswer");
        }
    }
}