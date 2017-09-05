using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using SceneLoader;
using Util;

namespace UIComponent
{
    public class MenuOutro : MonoBehaviour
    {
        [SerializeField]
        private Button mInstBtnHome = null;
        [SerializeField]
        private Button mInstBtnMiniGame = null;
        [SerializeField]
        private Button mInstBtnRetry = null;
        [SerializeField]
        private Button mInstBtnNext = null;

        private Func<bool> mHasNextEpisode = null;

        /**
         @fn    public void Initialize( Action onLoadMiniGame, Action onRetryEpisode, Action onNextEpisode, Func<bool> hasEnableNextEpisode)
        
         @brief Initializes this object
        
         @author    SEONG
         @date  2017-08-31
        
         @param onLoadMiniGame          미니게임 버튼 선택 시 실행.
         @param onRetryEpisode          다시하기 버튼 선택 시 실행.
         @param onNextEpisode           이어하기(다음 에피소드) 버튼 선택 시 실행.
         @param hasEnableNextEpisode    리턴 값에 따라 이어하기 버튼 활성화/비활성화.
         */
        public void Initialize(
            Action onLoadMiniGame,
            Action onRetryEpisode,
            Action onNextEpisode,
            Func<bool> isEnableNextEpisode)
        {
            mInstBtnHome.OnClickAsObservable().Subscribe(_ => SceneLoadWrapper.LoadScene(BuildScene.SceneHome));
            mInstBtnMiniGame.OnClickAsObservable().Subscribe(_ => onLoadMiniGame.SafeInvoke());
            mInstBtnRetry.OnClickAsObservable().Subscribe(_ => onRetryEpisode.SafeInvoke());
            mInstBtnNext.OnClickAsObservable().Subscribe(_ => onNextEpisode.SafeInvoke());
            mHasNextEpisode = isEnableNextEpisode;
        }

        public void Show()
        {
            bool isEnableNextEpisode = mHasNextEpisode.SafeInvoke(false);
            mInstBtnNext.enabled = isEnableNextEpisode;
            mInstBtnNext.image.color = isEnableNextEpisode ? Color.white : Color.black;
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
