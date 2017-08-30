using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UniRx;
using System.Text.RegularExpressions;
using CustomDebug;

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

        private void Awake()
        {

            mAuth = FirebaseAuth.DefaultInstance;

            InitializeFirebase();

            InstUI.SetScene(this);
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
                    mResultText.Value = result;
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
    }
}
