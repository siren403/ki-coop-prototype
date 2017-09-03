using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Home
{
    public class SceneHome : MonoBehaviour
    {
        public ViewHome View = null;

        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(50, 5);
            View.SetScene(this);
        }

    }
}
