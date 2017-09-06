using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Examples
{
    public class ExamAndroidPlugin : MonoBehaviour
    {
        private NonActivity mNonActivity = null;
        public Text InstTxtResult = null;

        public Button InstBtn1 = null;
        public Button InstBtn2 = null;

        private void Awake()
        {
            mNonActivity = new NonActivity("com.kids.charlie", "NonActivity");
            InstBtn1.OnClickAsObservable().Subscribe(_ => mNonActivity.GetString("unity_"));
            InstBtn2.OnClickAsObservable().Subscribe(_ => mNonActivity.GetInt(10));
        }

        private void OnDestroy()
        {
            mNonActivity.Dispose();
            mNonActivity = null;
        }

    }
}
