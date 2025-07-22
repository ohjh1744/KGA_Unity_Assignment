using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _userInfoText;
 

    public void GPGS_Login()
    {
        Debug.Log("�α��� �õ�!");
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) => {

            if (result == SignInStatus.Success)
            {
                string name = PlayGamesPlatform.Instance.GetUserDisplayName();
                string id = PlayGamesPlatform.Instance.GetUserId();
                string imgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();

                _userInfoText.text = $"�÷��̾�: {name}{id}";
            }
            else
            {
                _userInfoText.text = "Failed ";
            }
        });
    }

}
