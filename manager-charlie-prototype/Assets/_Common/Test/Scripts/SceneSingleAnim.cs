using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace KidsTest
{

    public class SceneSingleAnim : MonoBehaviour
    {
        public Button InstBtnReturn = null;

        private void Awake()
        {
            InstBtnReturn.OnClickAsObservable().Subscribe(_ => SceneManager.LoadScene("SceneTest"));
        }
    }
}
