using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void GameReStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    public void GameFinish()
    {
        Time.timeScale = 0;
        Debug.Log("게임이 종료되었습니다.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameReStart();
        }
    }
}
