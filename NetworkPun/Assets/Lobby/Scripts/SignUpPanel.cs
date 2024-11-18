using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SignUpPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField passwordInputFIeld;
    [SerializeField] TMP_InputField passwordConfirmInputFIeld;
    [SerializeField] TMP_Text checkText;

    private bool isCanUse;

    public void OnDisable()
    {
        emailInputField.text = "";
        passwordInputFIeld.text = "";
        passwordConfirmInputFIeld.text = "";
        checkText.text = "";
    }

    public void CheckEmail()
    {
        string email = emailInputField.text;
        BackendManager.Auth.FetchProvidersForEmailAsync(email).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("FetchProvidersForEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("FetchProvidersForEmailAsync was faulted.");
                return;
            }
            if (task.IsCompleted)
            {
                // �̸��Ͽ� ���� ��� ������ ������ ����Ʈ ��������
                var providers = task.Result;
                if (providers.Any())
                {
                    Debug.LogError("�̹� �����ϴ� ����.");
                    checkText.text = "Already Exists Email";
                    isCanUse = false;
                }
                else
                {
                    Debug.LogError("��� ������ ����.");
                    checkText.text = "Can Use Email";
                    isCanUse = true;
                }
                
            }

        });
    }

    public void SignUp()
    {
        string email = emailInputField.text;
        string pass = passwordInputFIeld.text;
        string confirm = passwordConfirmInputFIeld.text;

        if (isCanUse == false)
        {
            Debug.Log("�̸��� �ߺ�Ȯ���� �ʿ��մϴ�.");
            return;
        }

        //���̾�̽��� ��û�� ������ ���� ����.
        //continuewithonMainTHread�������. -> ����Ƽ�� ����������Ŭ�� �ֱ� ������, ���ν����带 �Ȱǵ帮�� UI�� ���̰� ������.
        //�޴��󿡴� ContinueWith�� �Ǿ��ֱ�������, �̰� �޴��� �߸�.
        BackendManager.Auth.CreateUserWithEmailAndPasswordAsync(email, pass).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }

            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled");            
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
            //ȸ������ �����ϸ� ������
            gameObject.SetActive(false);
        });
    }

}
