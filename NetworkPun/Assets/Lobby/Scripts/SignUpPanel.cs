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
                // 이메일에 대해 사용 가능한 제공자 리스트 가져오기
                var providers = task.Result;
                if (providers.Any())
                {
                    Debug.LogError("이미 존재하는 계정.");
                    checkText.text = "Already Exists Email";
                    isCanUse = false;
                }
                else
                {
                    Debug.LogError("사용 가능한 계정.");
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
            Debug.Log("이메일 중복확인이 필요합니다.");
            return;
        }

        //파이어베이스는 요청과 반응을 같이 구현.
        //continuewithonMainTHread사용하자. -> 유니티는 라이프사이클이 있기 때문에, 메인스레드를 안건드리면 UI가 꼬이고 터진다.
        //메뉴얼에는 ContinueWith로 되어있긴하지만, 이건 메뉴얼 잘못.
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
            //회원가입 성공하면 나가기
            gameObject.SetActive(false);
        });
    }

}
