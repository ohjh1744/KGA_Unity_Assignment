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
            Debug.LogWarning("�̸������Է����ּ���");
            return;
        }

        if(pass != confirm)
        {
            Debug.LogWarning("�н����尡 ��ġ���� �ʽ��ϴ�");
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
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }


            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.Log($"Firebase user created successfully: {result.User.DisplayName} ({result.User.UserId})");
            //ȸ������ �����ϸ� ������
            gameObject.SetActive(false);
        });
    }

    //public void checkEmail()
    //{
    //    string email = emailInputField.text;

    //    // �̸����� ��������� �ٷ� ����
    //    if (string.IsNullOrEmpty(email))
    //    {
    //        Debug.LogWarning("�̸����� �Է����ּ���.");
    //        return;
    //    }

    //    // �̸����� �̹� ��� �������� Ȯ��
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
    //                Debug.LogError($"�̸��� Ȯ�� �� ���� �߻�: {firebaseException.Message}");
    //            }
    //            else
    //            {
    //                Debug.LogError("FetchSignInMethodsForEmailAsync encountered an error: " + task.Exception);
    //            }
    //            return;
    //        }

    //        // �̸��Ͽ� ����� �α��� ����� ������ �̸����� �̹� ������
    //        List<string> signInMethods = task.Result;
    //        if (signInMethods.Count > 0)
    //        {
    //            Debug.LogError("�̹� �����ϴ� �̸����Դϴ�.");
    //            // �̸����� �̹� ��� ���̸� �߰����� ó���� �޽��� ǥ��
    //        }
    //        else
    //        {
    //            Debug.Log("��� ������ �̸����Դϴ�.");
    //            // �̸����� ������ ���� ��� �߰����� ó��
    //        }
    //    });
    //}
}
