using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AndroidPlugin
{
    public class AndroidObject : IDisposable
    {
        private string mObjectName = string.Empty;

        private AndroidJavaObject mJavaObject = null;
        public AndroidJavaObject JavaObject
        {
            get
            {
                return mJavaObject;
            }
        }

        public AndroidObject(string packageName,string className)
        {
            mObjectName = string.Format("{0}.{1}", packageName, className);
            Debug.Log(mObjectName);
#if UNITY_ANDROID && !UNITY_EDITOR
            mJavaObject = new AndroidJavaObject(mObjectName);
#endif
        }

        protected T CallMathod<T>(string methodName, params object[] args)
        {

#if UNITY_ANDROID && !UNITY_EDITOR
            return mJavaObject.Call<T>(methodName, args);
#else
            Debug.Log(string.Format("Call Method : {0}", methodName));
            return default(T);
#endif

        }
        protected T GetField<T>(string fieldName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return mJavaObject.Get<T>(fieldName);
#else
            Debug.Log(string.Format("Get Field : {0}", fieldName));
            return default(T);
#endif
        }
        protected void SetField<T>(string fieldName, T value)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            mJavaObject.Set<T>(fieldName, value);
#else
            Debug.Log(string.Format("Set Field : {0}, {1}", fieldName, value));
#endif
        }

        public virtual void Dispose()
        {
            if(mJavaObject != null)
            {
                mJavaObject.Dispose();
            }
        }
    }
}
