using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using CustomDebug;
using System;
using UniRx;

namespace AndroidPlugin
{
    public class AndroidManager : MonoSingletonBase<AndroidManager>
    {
        public const string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
        public const string CURRENT_ACTIVITY = "currentActivity";
        public const string ALARM_MODERATOR = "com.kids.charlie.AlarmModerator";

        private CompositeDisposable mDisposables = new CompositeDisposable();

        // Unity
        private AndroidJavaClass mUnityJavaClass = null;
        private AndroidJavaObject mUnityActivity = null;
        // Plugin
        //private AndroidJavaClass mLaunchActivity = null; 


        protected override void Awake()
        {
            base.Awake();

#if !UNITY_EDITOR && UNITY_ANDROID
            mUnityJavaClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
            mUnityActivity = mUnityJavaClass.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY);

            mDisposables.Add(mUnityJavaClass);
            mDisposables.Add(mUnityActivity);
#endif

        }

        public void SetAlram(int id, int hour, int min, int sec)
        {
            CDebug.LogFormat("ID:{0},Hour:{1},Minute:{2},Second:{3}",id, hour, min, sec);
#if !UNITY_EDITOR && UNITY_ANDROID
            try
            {
                using (AndroidJavaClass alarmModerator = new AndroidJavaClass(ALARM_MODERATOR))
                {
                    alarmModerator.CallStatic("setAlram", id, hour, min, sec, "ReceiveString");
                }
            }
            catch (System.Exception e)
            {
                CDebug.Log(e);
            }
#endif
        }

        public void ReceiveString(string data)
        {
            CDebug.LogFormat("Receive : {0}", data);
        }

        private void OnDestroy()
        {
            mDisposables.Dispose();
        }
    }
}
