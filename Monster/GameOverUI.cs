using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject pool;
    public GameObject manager;
    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        Destroy(pool);
        Destroy(manager);
        SceneManager.LoadScene("MainMenu");
    }

    

}
