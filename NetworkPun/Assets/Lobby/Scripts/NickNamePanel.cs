using Firebase.Auth;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using Photon.Pun;

public class NickNamePanel : MonoBehaviour
{
    [SerializeField] TMP_InputField nickNameInputField;


    public void Confirm()
    {
        string nickName = nickNameInputField.text;
        if(nickName == "")
        {
            Debug.LogWarning("닉네임을 설정해주세요.");
        }

        FirebaseUser user = BackendManager.Auth.CurrentUser;
        if (user == null)
            return;

        //만약 기존의 이름과 새로운 이름이 같다면 return
        if(user.DisplayName == nickNameInputField.text)
        {
            Debug.LogError("기존의 이름과 다른 새로운 이름을 설정해주세요");
            return;
        }

        //파이어베이스에서는 닉네임을 비어두는것을 오류로 보지 않음.
        UserProfile profile = new UserProfile();
        profile.DisplayName = nickName;

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("UpdateUserProfileAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("User profile updated successfully.");
            Debug.Log($"Display name: {user.DisplayName}");
            Debug.Log($"Email: {user.Email}");
            Debug.Log($"EmailVerified: {user.IsEmailVerified}");
            Debug.Log($"UserId: {user.UserId}");

            PhotonNetwork.LocalPlayer.NickName = nickName;
            PhotonNetwork.ConnectUsingSettings();
            gameObject.SetActive(false);
        });
    }


}
