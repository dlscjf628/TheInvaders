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
        
    }

    public void MainMenu()
    {
        Destroy(pool);
        Destroy(manager);
        SceneManager.LoadScene("MainMenu");
    }

    

}
