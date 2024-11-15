using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;
using static UnityEditor.ShaderData;

public class SignUpPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInputField;
    [SerializeField] TMP_InputField passwordInputFIeld;
    [SerializeField] TMP_InputField passwordConfirmInputFIeld;

    public void SignUp()
    {
        string email = emailInputField.text;
        string pass = passwordInputFIeld.text;
        string confirm = passwordConfirmInputFIeld.text;

        if(email.IsNullOrEmpty())
        {
            Debug.LogWarning("이메일을입력해주세요");
            return;
        }

        if(pass != confirm)
        {
            Debug.LogWarning("패스워드가 일치하지 않습니다");
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
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }


            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
            //회원가입 성공하면 나가기
            gameObject.SetActive(false);
        });
    }

    //public void checkEmail()
    //{
    //    string email = emailInputField.text;

    //    // 이메일이 비어있으면 바로 리턴
    //    if (string.IsNullOrEmpty(email))
    //    {
    //        Debug.LogWarning("이메일을 입력해주세요.");
    //        return;
    //    }

    //    // 이메일이 이미 사용 중인지를 확인
    //    BackendManager.Auth.FetchProvidersForEmailAsync(email).ContinueWithOnMainThread(task =>
    //    {
    //        if (task.IsCanceled)
    //        {
    //            Debug.LogError("FetchSignInMethodsForEmailAsync was canceled.");
    //            return;
    //        }

    //        if (task.IsFaulted)
    //        {
    //            FirebaseException firebaseException = task.Exception.Flatten().InnerException as FirebaseException;

    //            if (firebaseException != null)
    //            {
    //                Debug.LogError($"이메일 확인 중 오류 발생: {firebaseException.Message}");
    //            }
    //            else
    //            {
    //                Debug.LogError("FetchSignInMethodsForEmailAsync encountered an error: " + task.Exception);
    //            }
    //            return;
    //        }

    //        // 이메일에 연결된 로그인 방법이 있으면 이메일이 이미 존재함
    //        List<string> signInMethods = task.Result;
    //        if (signInMethods.Count > 0)
    //        {
    //            Debug.LogError("이미 존재하는 이메일입니다.");
    //            // 이메일이 이미 사용 중이면 추가적인 처리나 메시지 표시
    //        }
    //        else
    //        {
    //            Debug.Log("사용 가능한 이메일입니다.");
    //            // 이메일이 사용되지 않은 경우 추가적인 처리
    //        }
    //    });
    //}
}
