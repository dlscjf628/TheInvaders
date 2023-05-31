using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;

    public GameObject poolManager;

    public int level;
    //public float gameTime;
    //public float maxGameTime = 20f;

    public bool gameOver;

    public PoolManager pool;

    public GameObject[] boss;
    
    public InforMation info;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        info = gameObject.GetComponent<InforMation>();
        level = info.Stage;
        instance = this;
        //pool = GameObject.Find("PoolManager").GetComponent<PoolManager>();
    }


    void Update()
    {
        
    }

   
}
