using Firebase.Auth;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class VerifyPanel : MonoBehaviour
{

    [SerializeField] private GameObject nickNamePanel;
    private void OnEnable()
    {
        SendVerifyMail();
    }

    private void OnDisable()
    {
        if(checkVerifyRoutine != null)
        {
            StopCoroutine(checkVerifyRoutine);
        }
    }


    //이메일 인증
    private void SendVerifyMail()
    {
        FirebaseUser user = BackendManager.Auth.CurrentUser;
        user.SendEmailVerificationAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SendEmailVerificationAsync was canceled.");
                gameObject.SetActive(false);
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendEmailVerificationAsync encountered an error: " + task.Exception);
                gameObject.SetActive(false);
                return;
            }

            Debug.Log("Email sent successfully.");
            checkVerifyRoutine = StartCoroutine(CheckVerifyRoutine());
        });

    }

    Coroutine checkVerifyRoutine; 
    IEnumerator CheckVerifyRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(3f);

        while (true)
        {
            BackendManager.Auth.CurrentUser.ReloadAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("ReloadAsync was canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError($"ReloadAsync encountered an error: {task.Exception.Message}");
                    return;
                }

                //리로드해야 갱신이 됨.
                if (BackendManager.Auth.CurrentUser.IsEmailVerified == true)
                {
                    Debug.Log("인증 확인");
                    nickNamePanel.gameObject.SetActive(true);
                    gameObject.SetActive(false);

                }
                
            });

            //인증 확인 
            yield return delay;
        }
    }
}

