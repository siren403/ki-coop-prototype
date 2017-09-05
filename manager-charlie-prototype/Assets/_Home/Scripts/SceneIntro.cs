using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Home
{
    public class SceneIntro : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 30;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        private void Start()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("SceneHome");
            //UnityEngine.SceneManagement.SceneManager.LoadScene("ExamSceneAndroid");
        }
    }
}
