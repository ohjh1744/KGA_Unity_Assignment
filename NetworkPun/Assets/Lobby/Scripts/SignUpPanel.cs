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
        Debug.LogWarning("�̸����� �Է����ּ���");
        return;
    }

    if (pass != confirm)
    {
        Debug.LogWarning("�н����尡 ��ġ���� �ʽ��ϴ�");
        return;
    }

    // �̸��� �ߺ� ���θ� üũ (���� �������� ����)
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

        // �̸����� �̹� �ٸ� ������ ���ǰ� �ִ� ���
        if (providers.Any()) // �̸����� �̹� ��ϵ� ���
        {
            Debug.LogError("�� �̸����� �̹� ���Ե� �̸����Դϴ�.");
            checkText.text = "Already Exists Email";  // �̹� ���Ե� �̸����Դϴ� �޽���
            isCheck = false;
        }
        else
        {
            // �̸����� ������ ���� ��� (���� ������ ���� ����)
            Debug.Log("�� �̸����� ����� �� �ֽ��ϴ�.");
            checkText.text = "Can Use Email";  // �̸����� ����� �� �ֽ��ϴ� �޽���
            isCheck = true;
        }
    });
    }

    public void SignUp()
    {
        string email = emailInputField.text;
        string pass = passwordInputFIeld.text;
        string confirm = passwordConfirmInputFIeld.text;

        //���̾�̽��� ��û�� ������ ���� ����.
        //continuewithonMainTHread�������. -> ����Ƽ�� ����������Ŭ�� �ֱ� ������, ���ν����带 �Ȱǵ帮�� UI�� ���̰� ������.
        //�޴��󿡴� ContinueWith�� �Ǿ��ֱ�������, �̰� �޴��� �߸�.
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
                            Debug.LogError("�̹� �����ϴ� ����.");
                            checkText.text = "Already Exists Email";
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

            if ( task.IsCompleted)
            {
                // Firebase user has been created.
                AuthResult result = task.Result;
                Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
                //ȸ������ �����ϸ� ������
                gameObject.SetActive(false);
            }
        });
    }

}
