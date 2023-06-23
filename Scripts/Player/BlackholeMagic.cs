using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackholeMagic : MonoBehaviour
{

    Animator ani;

    void Start()
    {

        ani = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
