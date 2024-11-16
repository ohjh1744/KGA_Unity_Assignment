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

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.LogError("��� ������ ����.");
            Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
            //ȸ������ �����ϸ� ������
            gameObject.SetActive(false);
        });
    }

}
