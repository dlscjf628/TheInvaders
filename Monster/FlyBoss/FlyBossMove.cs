using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBossMove : MonoBehaviour
{
    GameObject player;

    [SerializeField] private FlyBossState flyState;
    [SerializeField] private FlyBossAttack flyAttack;

    public float rushSpeed = 15f;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.gameOver && flyAttack.isLive)
        {
            if (t < 1.5f)
            {
                t += Time.deltaTime;
            }
            else
            {
                Move();
            }
        }
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (!flyAttack.rushTrue)
        {
            transform.Translate(direction * flyState.speed * Time.deltaTime);
        }
        else
        {
            if (!flyAttack.rushTime)
            {
                flyAttack.rushTime = true;
                flyAttack.rushPosition = (player.transform.position - transform.position).normalized;
            }
            transform.Translate(flyAttack.rushPosition * rushSpeed * Time.deltaTime);
        }
    }



    

}
