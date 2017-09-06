using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{

    public abstract class MonoSingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T mInst = null;
        private static bool mIsApplicationQuit = false;
        public static T Inst
        {
            get
            {
                if (mIsApplicationQuit)
                    return null;

                InstanceInitialize();
                
                return mInst;
            }
        }
        private static void InstanceInitialize()
        {
            if (mInst == null)
            {
                var objects = FindObjectsOfType<T>();

                if (objects.Length == 1)
                    mInst = objects[0];

                if (objects.Length > 1)
                    Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");

                if (mInst == null)
                {
                    string objectName = typeof(T).ToString();
                    GameObject go = GameObject.Find(objectName);
                    if (go == null)
                    {
                        go = new GameObject(objectName);
                    }
                    mInst = go.AddComponent<T>();
                }

            }
        }

        public bool AutoInitialize = false;
        public bool DontDestroy = false;

        protected virtual void Awake()
        {
            if(AutoInitialize)
            {
                InstanceInitialize();
                if (DontDestroy)
                {
                    DontDestroyOnLoad(this.gameObject);
                }
            }
        }
       

        protected virtual void OnApplicationQuit()
        {
            mIsApplicationQuit = true;
        }
    }
}
