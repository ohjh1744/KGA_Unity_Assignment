using Photon.Pun;
using TMPro;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Firebase;
using System;

// ���� �α��� ȭ�� ���� ����
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
                            Debug.LogError("��ȿ���� �ʴ� �̸������� �Դϴ�.");
                            break;
                        case AuthError.UserNotFound:
                            Debug.LogError("�������� �ʴ� �����Դϴ�.");
                            break;
                        case AuthError.WrongPassword:
                            Debug.LogError("��й�ȣ�� �߸��Ǿ����ϴ�.");
                            break;
                        default:
                            Debug.LogError("Firebase �α��� �� �� �� ���� ������ �߻��߽��ϴ�: " + firebaseException.Message);
                            break;
                    }
                }
                else
                {
                    Debug.LogError("FirebaseException�� �ƴ� �ٸ� ���ܰ� �߻��߽��ϴ�: " + exception?.ToString());
                }

                return;
            }

            //�α��μ����ϸ�
            AuthResult result = task.Result;
            Debug.Log($"User signed in successfully: {result.User.DisplayName} ({result.User.UserId})");
            CheckUserInfo();
        });
    }

    //������ ��������
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
            //TODO : �̸��� ���� ����
            verifyPanel.gameObject.SetActive(true);

        }
        else if (user.DisplayName == "")
        {
            //TODO : �г��� ���� ����
            nickNamePanel.gameObject.SetActive(true);
        }
        else
        {
            PhotonNetwork.LocalPlayer.NickName = user.DisplayName;
            PhotonNetwork.ConnectUsingSettings();
        }

    }
}
