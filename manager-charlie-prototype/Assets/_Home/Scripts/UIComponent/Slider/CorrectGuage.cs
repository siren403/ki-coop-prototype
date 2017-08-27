using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIComponent
{
    public class CorrectGuage : MonoBehaviour
    {
        public Image Background = null;
        public Image Foreground = null;

        
        public float Value
        {
            set
            {
                Foreground.fillAmount = Mathf.Clamp(value * 0.5f, 0, 0.5f);
            }
            get
            {
                return Foreground.fillAmount;
            }
        }
       
    }
}
