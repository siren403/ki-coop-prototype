using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KidsTest
{
    public class SceneTest : MonoBehaviour
    {
        public Button InstBtnObjects = null;
        public Button InstBtnSingleAnim = null;

        private void Awake()
        {
            InstBtnObjects.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene("SceneObjects"));
            InstBtnSingleAnim.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene("SceneSingleAnim"));
        }
    }
}
