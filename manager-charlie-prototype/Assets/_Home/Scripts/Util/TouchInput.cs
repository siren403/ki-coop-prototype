using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public static class TouchInput 
    {

        public static Vector2 GetPosition()
        {
            Vector2 position = Vector2.zero;
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    position = Input.GetTouch(0).position;
                }
            }
            else
            {
                position = Input.mousePosition;
            }
            return position;
        }
        public static bool Begin()
        {
            bool isBegin = false;

            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    isBegin = Input.GetTouch(0).phase == TouchPhase.Began;
                }
            }
            else
            {
                isBegin = Input.GetMouseButtonDown(0);
            }

            return isBegin;
        }

        public static bool Ended()
        {
            bool isEnded = false;
            if (Application.platform == RuntimePlatform.Android)
            {
                if (Input.touchCount > 0)
                {
                    isEnded = Input.GetTouch(0).phase == TouchPhase.Ended;
                }
            }
            else
            {
                isEnded = Input.GetMouseButtonUp(0);
            }

            return isEnded;
        }
    }
}
