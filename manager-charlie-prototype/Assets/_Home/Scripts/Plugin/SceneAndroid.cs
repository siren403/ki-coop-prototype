using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomDebug;

namespace Examples.Plugin
{
    public class SceneAndroid : MonoBehaviour
    {

        #region
        public const string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
        public const string CURRENT_ACTIVITY = "currentActivity";
        public const string RUN_UI_THREAD = "runOnUiThread";
        private static AndroidJavaObject activity
        {
            get
            {
                AndroidJavaClass androidJavaClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
                return androidJavaClass.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY);
            }
        }

        public static bool IsInstalledApp(string packageName)
        {
            if (Application.platform != RuntimePlatform.Android) return false;
            try
            {
                activity
                    .Call<AndroidJavaObject>("getPackageManager")
                    .Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void ShowToast(string msg, bool isShortLength)
        {
            activity.Call(RUN_UI_THREAD, new AndroidJavaRunnable(() =>
            {
                AndroidJavaObject toastObject
                    = new AndroidJavaObject("android.widget.Toast", activity);
                toastObject
                    .CallStatic<AndroidJavaObject>("makeText", activity, msg, (isShortLength ? 0 : 1))
                    .Call("show");
            }));
        }

        public T CallNativeMathod<T>(string methodName, params object[] args)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass cls = new AndroidJavaClass(UNITY_PLAYER_CLASS))
            {
                using (AndroidJavaObject obj = cls.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY))
                {
                    T value = obj.Call<T>(methodName, args);
                    return value;
                }
            }
#elif UNITY_EDITOR
            CDebug.LogFormat("Call Native Method : {0}", methodName);
            return default(T);
#endif
        }


        #endregion

    }
}
