using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UniRx;
using System.Text.RegularExpressions;
using CustomDebug;
using Util;
namespace Examples.Plugin
{
    public class SceneFirebase : MonoBehaviour
    {
        private string mAccount = string.Empty;
        private string mPassword = string.Empty;

        public string Account { get { return mAccount; } set { mAccount = value; } }
        public string Password { get { return mPassword; }set { mPassword = value; } }

        private StringReactiveProperty mResultText = new StringReactiveProperty();
        public ReadOnlyReactiveProperty<string> ResultText
        {
            get
            {
                return mResultText.ToReadOnlyReactiveProperty();
            }
        }

        private FirebaseAuth mAuth = null;
        private FirebaseUser mCurrentUser = null;

        public UIFirebase InstUI = null;

        public UnityEngine.UI.Text InstTxtPhoneNumber = null;

        private string PhoneAuthCode = string.Empty;
        private string PhoneAuthID = string.Empty;
        private PhoneAuthProvider mPhoneAuthProvider = null;

        private void Awake()
        {

            mAuth = FirebaseAuth.DefaultInstance;

            InitializeFirebase();

            InstUI.SetScene(this);

            //var phoneNumber = CallNativeMathod<string>("GetPhoneNumber", false);
            //InstTxtPhoneNumber.text = phoneNumber;
        }


        public void CreateUser()
        {
            bool isValidAccount = IsValidEmail(mAccount);
            bool isValidPassword = IsvalidPassword(mPassword);
            string txtValid = string.Empty;
            if (isValidAccount)
            {
                txtValid += "\n유효 이메일 형식";
                if (isValidPassword)
                {
                    txtValid += "\n유효 패스워드";
                }
                else
                {
                    txtValid += "\n유효하지 않은 패스워드";
                }
            }
            else
            {
                txtValid += "\n유효하지 않은 이메일";
            }
            Print(txtValid);
            mResultText.Value = txtValid;
            if (isValidAccount && isValidPassword)
            {
                mAuth.CreateUserWithEmailAndPasswordAsync(mAccount, mPassword).ContinueWith(task => {
                    if (task.IsCanceled)
                    {
                        //Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                        Print("CreateUserWithEmailAndPasswordAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        //Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        Print("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);

                        return;
                    }

                    // Firebase user has been created.
                    mCurrentUser = task.Result;
                    string result = string.Format("Firebase user created successfully: {0} ({1})",
                        mCurrentUser.DisplayName, mCurrentUser.UserId);
                    Print(result);
                });
            }
        }
        private bool IsValidEmail(string email)
        {
            bool valid = Regex.IsMatch(email, "[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?");
            return valid;
        }
        private bool IsvalidPassword(string password)
        {
            return true;
            bool valid = Regex.IsMatch(password, @"/^.*(?=^.{8,15}$)(?=.*\d)(?=.*[a-zA-Z])(?=.*[!@#$%^&+=]).*$/");
            //영대/소문자, 숫자 및 특수문자 조합 비밀번호 8자리이상 15자리 이하
            if (valid == false)
            {
                CDebug.Log("영대/소문자, 숫자 및 특수문자 조합 비밀번호 8자리이상 15자리 이하.");
            }
            return valid;
        }

        public void Login()
        {
            mAuth.SignInWithEmailAndPasswordAsync(mAccount, mPassword).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    //Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    Print("SignInWithEmailAndPasswordAsync was canceled.");

                    return;
                }
                if (task.IsFaulted)
                {
                    //Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    Print("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                mCurrentUser = task.Result;
                string result = string.Format("User signed in successfully: {0} ({1})",
                    mCurrentUser.DisplayName, mCurrentUser.UserId);
                Print(result);
                mResultText.Value = result;
            });
        }


        public void RequestPhoneAuthCode()
        {
            mPhoneAuthProvider = PhoneAuthProvider.GetInstance(mAuth);
            string phoneNumber = "";
            uint timeOut = 30000;

            phoneNumber = CallNativeMathod<string>("GetPhoneNumber",false);
            InstTxtPhoneNumber.text = phoneNumber;

            mPhoneAuthProvider.VerifyPhoneNumber(phoneNumber, timeOut, null,
                verificationCompleted: (credential) =>
                {
                    // 자동적으로 SMS를 확인하여 입력 완료
                    // 인증코드를 입력 하지 않아도 됨
                    // 안드로이드만 해당
                    // credential은 GetCredenrial을 호출하는 대신 사용
                    mResultText.Value = credential.Provider;
                },
                verificationFailed: (error) => 
                {
                    // 인증 코드가 전송되지 않았을 수 있음
                    // error에 관련 내용 담겨있음
                    mResultText.Value = error;
                },
                codeSent:(id, token)=> 
                {
                    // 인증 코드가 SMS를 통해 성공적으로 전송되었습니다.
                    // 'id`에는 전달해야 할 확인 ID가 들어 있습니다.
                    // GetCredential ()을 호출 할 때 사용자의 코드.
                    // 사용자가 코드를 다시 보내달라고 요청하면 `token`을 사용할 수 있습니다.
                    // 두 요청을 하나로 묶습니다.
                    PhoneAuthID = id;
                    mResultText.Value = "코드 전달 성공";
                },
                codeAutoRetrievalTimeOut:(id)=> 
                {
                    // 자동 sms 검색이 지정된 시간에 기반하여 타임 아웃되었을 때 호출됩니다.
                    // timeout 매개 변수.
                    //`id`는 타임 아웃 한 요청의 확인 ID를 포함합니다.
                    mResultText.Value = "타임 아웃";
                });
        }
        public void AuthPhone(string code)
        {
            PhoneAuthCode = code;
            var credential = mPhoneAuthProvider.GetCredential(PhoneAuthID, PhoneAuthCode);
            InternalSigninPhone(credential);
        }
        private void InternalSigninPhone(Credential credential)
        {
            mAuth.SignInWithCredentialAsync(credential).ContinueWith(task => {
                if (task.IsFaulted)
                {
                    //Debug.LogError("SignInWithCredentialAsync encountered an error: " +
                    //               task.Exception);
                    Print("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                mCurrentUser = task.Result;
                string result = string.Format("User signed in successfully: {0} \n({1})",
                    mCurrentUser.PhoneNumber, mCurrentUser.ProviderId);
                Print(result);
            });
        }

        void InitializeFirebase()
        {
            mAuth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            mAuth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

        void AuthStateChanged(object sender, System.EventArgs eventArgs)
        {
            if (mAuth.CurrentUser != mCurrentUser)
            {
                bool signedIn = mCurrentUser != mAuth.CurrentUser && mAuth.CurrentUser != null;
                if (!signedIn && mCurrentUser != null)
                {
                    Print("Signed out " + mCurrentUser.UserId);
                }
                mCurrentUser = mAuth.CurrentUser;
                if (signedIn)
                {
                    Print(string.Format("Signed in {0}\nName : {1}\nEmail : {2}\nPhotoUrl : {3}",
                        mCurrentUser.UserId,
                        mCurrentUser.DisplayName,
                        mCurrentUser.Email,
                        mCurrentUser.PhotoUrl));

                }
            }
        }

        public void Logout()
        {
            mAuth.SignOut();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        public void CreateGuest()
        {
            mAuth.SignInAnonymouslyAsync().ContinueWith(task => {
                if (task.IsCanceled)
                {
                    //Debug.LogError("SignInAnonymouslyAsync was canceled.");
                    Print("SignInAnonymouslyAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    //Debug.LogError("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    Print("SignInAnonymouslyAsync encountered an error: " + task.Exception);
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Print(string.Format("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId));
            });
        }

        public void CallFromNative(string str)
        {
            mResultText.Value = str;
        }


        private void Print(string str)
        {
            CDebug.Log(str);
            mResultText.Value = str;
        }


        #region
        public const string UNITY_PLAYER_CLASS = "com.unity3d.player.UnityPlayer";
        public const string CURRENT_ACTIVITY = "currentActivity";
        public const string RUN_UI_THREAD = "runOnUiThread";
        //private static AndroidJavaObject activity
        //{
        //    get
        //    {
        //        AndroidJavaClass androidJavaClass = new AndroidJavaClass(UNITY_PLAYER_CLASS);
        //        return androidJavaClass.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY);
        //    }
        //}

        //public static bool IsInstalledApp(string packageName)
        //{
        //    if (Application.platform != RuntimePlatform.Android) return false;
        //    try
        //    {
        //        activity
        //            .Call<AndroidJavaObject>("getPackageManager")
        //            .Call<AndroidJavaObject>("getPackageInfo", packageName, 0);
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static void ShowToast(string msg, bool isShortLength)
        //{
        //    activity.Call(RUN_UI_THREAD, new AndroidJavaRunnable(() =>
        //    {
        //        AndroidJavaObject toastObject
        //            = new AndroidJavaObject("android.widget.Toast", activity);
        //        toastObject
        //            .CallStatic<AndroidJavaObject>("makeText", activity, msg, (isShortLength ? 0 : 1))
        //            .Call("show");
        //    }));
        //}

        public T CallNativeMathod<T>(string methodName, params object[] args)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (AndroidJavaClass cls = new AndroidJavaClass(UNITY_PLAYER_CLASS))
            {
                using (AndroidJavaObject obj = cls.GetStatic<AndroidJavaObject>(CURRENT_ACTIVITY))
                {
                    T value = obj.Call<T>(methodName, args);
                    return value;
                }
            }
#elif UNITY_EDITOR
            CDebug.LogFormat("Call Native Method : {0}", methodName);
            return default(T);
#endif
        }

        
#endregion
    }
}
