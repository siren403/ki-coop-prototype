using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UIComponent
{
    public class CorrectGauge : MonoBehaviour
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
                return Mathf.Clamp(Foreground.fillAmount * 2.0f, 0, 1.0f);
            }
        }


        public Tween TweenValue(float value, float duration)
        {
            return DOTween.To(() => this.Value, (x) => this.Value = x, value, duration);
        }
    }
}
