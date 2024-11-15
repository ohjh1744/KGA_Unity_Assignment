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
            Debug.LogWarning("�г����� �������ּ���.");
        }

        FirebaseUser user = BackendManager.Auth.CurrentUser;
        if (user == null)
            return;

        //���� ������ �̸��� ���ο� �̸��� ���ٸ� return
        if(user.DisplayName == nickNameInputField.text)
        {
            Debug.LogError("������ �̸��� �ٸ� ���ο� �̸��� �������ּ���");
            return;
        }

        //���̾�̽������� �г����� ���δ°��� ������ ���� ����.
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
