using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Util;
using CustomDebug;
using System;
using DG.Tweening;
using UniRx;

namespace UIComponent
{
    public class GridSwipe : MonoBehaviour
    {
        private Vector2 mBeginPosition = Vector2.zero;

        private int mCurrentPage = 0;
        private Func<int> mMaximumPage = null;

        public Grid TargetGrid = null;
        public float Duration = 0.3f;
        public Ease EasingType = Ease.OutExpo;
        public int CurrentPage
        {
            get
            {
                return mCurrentPage;
            }
        }


        public Button InstBtnLeft = null;
        public Button InstBtnRight = null;

        private bool mIsGesture = true;

        private void Awake()
        {
            if(InstBtnLeft != null && InstBtnRight != null)
            {
                mIsGesture = false;
                InstBtnLeft.OnClickAsObservable().Subscribe(_ => OnSwipe(-1));
                InstBtnRight.OnClickAsObservable().Subscribe(_ => OnSwipe(1));
            }
        }

        private void Update()
        {
            if (mIsGesture == false)
                return;

            if (TouchInput.Begin())
            {
                mBeginPosition = TouchInput.GetPosition();
            }
            if (TouchInput.Ended())
            {
                if((TouchInput.GetPosition()-mBeginPosition).normalized.x > 0)
                {
                    OnSwipe(-1);
                }
                else
                {
                    OnSwipe(1);
                }
            }
        }
        private void OnSwipe(int dir)
        {
            //DOTween.Kill(TargetGrid.transform);
            if (DOTween.IsTweening(TargetGrid.transform))
                return;

            mCurrentPage = Mathf.Clamp(mCurrentPage + dir, 0, TargetGrid.MaximumPage - 1);

            TargetGrid.transform.DOLocalMoveX(-mCurrentPage * TargetGrid.PageDistance, Duration)
                .SetEase(EasingType)
                .SetId(TargetGrid.transform);
        }
    }
}
