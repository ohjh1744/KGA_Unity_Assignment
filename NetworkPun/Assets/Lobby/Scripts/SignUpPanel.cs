using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using WebSocketSharp;
using static UnityEditor.ShaderData;

public class SignUpPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField passwordInputFIeld;
    [SerializeField] TMP_InputField passwordConfirmInputFIeld;
    [SerializeField] TMP_Text checkText;

    private bool isCheck;

    //public void CheckEmail()
    //{
    //    string email = emailInputField.text;
    //    BackendManager.Auth.FetchProvidersForEmailAsync(email).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            Debug.Log("Provider fetch canceled.");
    //        }
    //        else if (task.IsFaulted)
    //        {
    //            Debug.Log("Provider fetch encountered an error.");
    //            Debug.Log(task.Exception.ToString());
    //        }
    //        else if (task.IsCompleted)
    //        {
    //            Debug.Log("Email Providers:");
    //            foreach (string provider in task.Result)
    //            {
    //                Debug.Log(provider);
    //            }
    //        }

    //    });
    //}

    public void CheckEmail()
    {
       string email = emailInputField.text;
    string pass = passwordInputFIeld.text;
    string confirm = passwordConfirmInputFIeld.text;

    if (email.IsNullOrEmpty())
    {
        Debug.LogWarning("이메일을 입력해주세요");
        return;
    }

    if (pass != confirm)
    {
        Debug.LogWarning("패스워드가 일치하지 않습니다");
        return;
    }

    // 이메일 중복 여부만 체크 (계정 생성하지 않음)
    BackendManager.Auth.FetchProvidersForEmailAsync(email).ContinueWithOnMainThread(task =>
    {
        if (task.IsCanceled)
        {
            Debug.LogError("FetchProvidersForEmailAsync was canceled.");
            return;
        }

        if (task.IsFaulted)
        {
            Debug.LogError("FetchProvidersForEmailAsync encountered an error: " + task.Exception);
            checkText.text = "Please enter a valid email.";
            return;
        }

        var providers = task.Result;

        // 이메일이 이미 다른 계정에 사용되고 있는 경우
        if (providers.Any()) // 이메일이 이미 등록된 경우
        {
            Debug.LogError("이 이메일은 이미 가입된 이메일입니다.");
            checkText.text = "Already Exists Email";  // 이미 가입된 이메일입니다 메시지
            isCheck = false;
        }
        else
        {
            // 이메일이 사용되지 않은 경우 (계정 생성은 하지 않음)
            Debug.Log("이 이메일은 사용할 수 있습니다.");
            checkText.text = "Can Use Email";  // 이메일을 사용할 수 있습니다 메시지
            isCheck = true;
        }
    });
    }

    public void SignUp()
    {
        string email = emailInputField.text;
        string pass = passwordInputFIeld.text;
        string confirm = passwordConfirmInputFIeld.text;

        //파이어베이스는 요청과 반응을 같이 구현.
        //continuewithonMainTHread사용하자. -> 유니티는 라이프사이클이 있기 때문에, 메인스레드를 안건드리면 UI가 꼬이고 터진다.
        //메뉴얼에는 ContinueWith로 되어있긴하지만, 이건 메뉴얼 잘못.
        BackendManager.Auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("FetchProvidersForEmailAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Exception exception = task.Exception.InnerException;

                FirebaseException firebaseException = exception as FirebaseException;

                if (firebaseException != null)
                {
                    AuthError errorCode = (AuthError)firebaseException.ErrorCode;
                    Debug.Log(errorCode.ToString());

                    switch (errorCode)
                    {
                        case AuthError.EmailAlreadyInUse:
                            Debug.LogError("이미 존재하는 계정.");
                            checkText.text = "Already Exists Email";
                            break;
                        default:
                            Debug.LogError("Firebase 로그인 중 알 수 없는 오류가 발생했습니다: " + firebaseException.Message);
                            break;
                    }
                }
                else
                {
                    Debug.LogError("FirebaseException이 아닌 다른 예외가 발생했습니다: " + exception?.ToString());
                }
                return;
            }

            if ( task.IsCompleted)
            {
                // Firebase user has been created.
                AuthResult result = task.Result;
                Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
                //회원가입 성공하면 나가기
                gameObject.SetActive(false);
            }
        });
    }

}
