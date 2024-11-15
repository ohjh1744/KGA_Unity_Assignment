using Photon.Pun;
using TMPro;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;
using System;

// 먼저 로그인 화면 부터 시작
public class LoginPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField passwordInputField;

    [SerializeField] GameObject nickNamePanel;
    [SerializeField] GameObject verifyPanel;
    public void Login()
    {
        string email = emailInputField.text;
        string password = passwordInputField.text;

        BackendManager.Auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
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
                        case AuthError.InvalidEmail:
                            Debug.LogError("유효하지 않는 이메일형식 입니다.");
                            break;
                        case AuthError.UserNotFound:
                            Debug.LogError("존재하지 않는 계정입니다.");
                            break;
                        case AuthError.WrongPassword:
                            Debug.LogError("비밀번호가 잘못되었습니다.");
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

            //로그인성공하면
            AuthResult result = task.Result;
            Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
            CheckUserInfo();
        });
    }

    //프로필 가져오기
    private void CheckUserInfo()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        Debug.Log($"Display name: {user.DisplayName}");
        Debug.Log($"Email: {user.Email}");
        Debug.Log($"EmailVerified: {user.IsEmailVerified}");
        Debug.Log($"UserId: {user.UserId}");

        if (user.IsEmailVerified == false)
        {
            //TODO : 이메일 인증 진행
            verifyPanel.gameObject.SetActive(true);

        }
        else if (user.DisplayName == "")
        {
            //TODO : 닉네임 설정 진행
            nickNamePanel.gameObject.SetActive(true);
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = user.DisplayName;
            PhotonNetwork.ConnectUsingSettings();
        }

    }
}
