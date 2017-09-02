using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Text.RegularExpressions;

namespace Examples.Plugin
{
    public class UIFirebase : MonoBehaviour
    {
        private SceneFirebase mScene = null;

        public Text InstTxtResult = null;

        /** @brief Navigation */
        public GameObject InstPanelNavigation = null;
        public Button InstBtnOpenEmail = null;
        public Button InstBtnOpenGuest = null;
        public Button InstBtnOpenPhone = null;

        /** @brief Email*/
        public GameObject InstPanelEmail = null;
        public InputField InstInputAccount = null;
        public InputField InstInputPassword = null;
        public Button InstBtnCreateUser = null;
        public Button InstBtnLogin = null;
        public Button InstBtnLogout = null;


        /** @brief Guest */
        public GameObject InstPanelGuest = null;
        public Button InstBtnGuestLogin = null;
        public Button InstBtnGuestSignOut = null;

        /** @brief phone */
        public GameObject InstPanelPhone = null;
        public Button InstBtnReqCode = null;
        public InputField InstInputPhoneAuthCode = null;
        public Button InstBtnPhoneAuth = null;

        public void SetScene(SceneFirebase scene)
        {
            mScene = scene;

            mScene.ResultText.Subscribe(result => InstTxtResult.text = result);

            InstBtnOpenEmail.OnClickAsObservable().Subscribe(_ =>
            {
                InstPanelNavigation.SetActive(false);
                InstPanelEmail.SetActive(true);
            });
            InstInputAccount.OnValueChangedAsObservable().Subscribe((value) => mScene.Account = value);
            InstInputPassword.OnValueChangedAsObservable().Subscribe((value) => mScene.Password = value);

            InstBtnCreateUser.OnClickAsObservable()
                .Where(_ => string.IsNullOrEmpty(InstInputAccount.text) == false && string.IsNullOrEmpty(InstInputPassword.text) == false)
                .Subscribe(_ => mScene.CreateUser());
            InstBtnLogin.OnClickAsObservable()
                .Where(_ => string.IsNullOrEmpty(InstInputAccount.text) == false && string.IsNullOrEmpty(InstInputPassword.text) == false)
                .Subscribe(_ => mScene.Login());
            InstBtnLogout.OnClickAsObservable().Subscribe(_ => mScene.Logout());


            InstBtnOpenGuest.OnClickAsObservable().Subscribe(_ =>
            {
                InstPanelNavigation.SetActive(false);
                InstPanelGuest.SetActive(true);
            });
            InstBtnGuestLogin.OnClickAsObservable().Subscribe(_ => 
            {
                mScene.CreateGuest();
            });
            InstBtnGuestSignOut.OnClickAsObservable().Subscribe(_ => mScene.SignOut());


            InstBtnOpenPhone.OnClickAsObservable().Subscribe(_ => 
            {
                InstPanelNavigation.SetActive(false);
                InstPanelPhone.SetActive(true);
            });
            InstBtnReqCode.OnClickAsObservable().Subscribe(_ => mScene.RequestPhoneAuthCode());
            InstBtnPhoneAuth.OnClickAsObservable().Subscribe(_ => 
            {
                mScene.AuthPhone(InstInputPhoneAuthCode.text.Trim());
            });
        }

        
    }
}
