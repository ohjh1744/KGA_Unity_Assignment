using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }
    public void GameReStart()
    {
        SceneManager.LoadScene(0);
    }


    public void GameFinish()
    {
        Time.timeScale = 0;
        Debug.Log("������ ����Ǿ����ϴ�.");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameReStart();
        }
    }
}
