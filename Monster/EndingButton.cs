using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingButton : MonoBehaviour
{
    GameObject pool;
    GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        pool = GameObject.Find("PoolManager");
        manager = GameObject.Find("GameManager");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        Destroy(pool);
        Destroy(manager);
        SceneManager.LoadScene("MainMenu");
    }

}
