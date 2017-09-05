using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;
using UnityEngine.UI;
using UniRx;

namespace Examples
{
    public class ExamSceneAndroid : MonoBehaviour
    {
        public const string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
        public const string CURRENT_ACTIVITY = "currentActivity";
        public const string RUN_UI_THREAD = "runOnUiThread";
        private static AndroidJavaClass mUnityJavaClass = null;
        private static AndroidJavaObject mUnityActivity = null;
        private static AndroidJavaObject UnityActivity
        {
            get
            {
                if (mUnityActivity == null)
                {
                    mUnityJavaClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
                    mUnityActivity = mUnityJavaClass.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY);
                }
                return mUnityActivity;
            }
        }
        #region
        //        public const string RUN_UI_THREAD = "runOnUiThread";
        //        private static AndroidJavaObject activity
        //        {
        //            get
        //            {
        //                AndroidJavaClass androidJavaClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
        //                return androidJavaClass.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY);
        //            }
        //        }

        //        public static bool IsInstalledApp(string packageName)
        //        {
        //            if (Application.platform != RuntimePlatform.Android) return false;
        //            try
        //            {
        //                activity
        //                    .Call<AndroidJavaObject>("getPackageManager")
        //                    .Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
        //                return true;
        //            }
        //            catch
        //            {
        //                return false;
        //            }
        //        }
        //        public static void ShowToast(string msg, bool isShortLength)
        //        {
        //            activity.Call(RUN_UI_THREAD, new AndroidJavaRunnable(() =>
        //            {
        //                AndroidJavaObject toastObject
        //                    = new AndroidJavaObject("android.widget.Toast", activity);
        //                toastObject
        //                    .CallStatic<AndroidJavaObject>("makeText", activity, msg, (isShortLength ? 0 : 1))
        //                    .Call("show");
        //            }));
        //        }

        //        public T CallNativeMathod<T>(string methodName, params object[] args)
        //        {
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //            using (AndroidJavaClass cls = new AndroidJavaClass(UNITY_PLAYER_CLASS))
        //            {
        //                using (AndroidJavaObject obj = cls.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY))
        //                {
        //                    T value = obj.Call<T>(methodName, args);
        //                    return value;
        //                }
        //            }
        //#elif UNITY_EDITOR
        //            CDebug.LogFormat("Call Native Method : {0}", methodName);
        //            return default(T);
        //#endif
        //        }


        #endregion

        public Text InstTxtResult = null;
        public Button InstBtnStartActivity = null;
        public Button InstBtnShowToast = null;

        public LayoutAlarm InstLayoutAlarm = null;

        private void Awake()
        {
            InstLayoutAlarm.Initialize();

            InstLayoutAlarm.BtnSetAlarm.OnClickAsObservable()
                 .Subscribe(_=> 
                 {
                     string strHour = InstLayoutAlarm.InputHour.text;
                     string strMin = InstLayoutAlarm.InputMin.text;
                     string strSec = InstLayoutAlarm.InputSec.text;

                     if (!string.IsNullOrEmpty(strHour) && 
                         !string.IsNullOrEmpty(strMin) && 
                         !string.IsNullOrEmpty(strMin))
                     {
                         int hour = System.Convert.ToInt32(strHour);
                         int min = System.Convert.ToInt32(InstLayoutAlarm.InputMin.text);
                         int sec = System.Convert.ToInt32(InstLayoutAlarm.InputSec.text);
                         SetAlram(hour, min, sec);
                     }
                     else
                     {

                     }
                 });
        }

        public void LaunchActivity()
        {
            try
            {
                using (AndroidJavaClass plugin = new AndroidJavaClass("com.kids.charlie.ActivityLauncher"))
                {
                    plugin.CallStatic("launchActivity", "com.kids.charlie.NativeActivity", UnityActivity);
                }
            }
            catch (System.Exception e)
            {
                InstTxtResult.text = e.ToString();
            }
        }


        private void SetAlram(int hour, int min, int sec)
        {
            CDebug.LogFormat("Hour:{0},Minute:{1},Second:{2}", hour, min, sec);
            try
            {
                using (AndroidJavaClass plugin = new AndroidJavaClass("com.kids.charlie.ActivityLauncher"))
                {
                    plugin.CallStatic("setAlram", 123, hour, min, sec, UnityActivity);
                }
            }
            catch (System.Exception e)
            {
                InstTxtResult.text = e.ToString();
            }
        }

        private void OnDestroy()
        {
            if(mUnityActivity != null)
            {
                mUnityActivity.Dispose();
            }
            if(mUnityJavaClass != null)
            {
                mUnityJavaClass.Dispose();
            }
        }
    }
}
