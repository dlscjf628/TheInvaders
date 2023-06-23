using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 0;
    Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = gameObject.transform.GetChild(0).GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerMoveAnimation()
    {
        ani.SetTrigger("Run");
    }
}
