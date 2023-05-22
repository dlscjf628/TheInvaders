using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonoBehaviour
{
    MonsterState mState;
    ViperHard viper;

    GameObject player;

    Vector3 rushPosition;

    public bool hard;
    public bool rushTime;

    float t = 7f;

    float monsterX, monsterY, monsterZ;
    // Start is called before the first frame update
    void Start()
    {
        mState = GetComponent<MonsterState>();
        viper = GetComponent<ViperHard>();
        player = GameObject.Find("Player");
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hard();
    }

    void Move()
    { 
        Vector3 direction = (player.transform.position - transform.position).normalized;

        if (!viper.rush)
        {
            transform.Translate(direction * mState.speed * Time.deltaTime);
        }
        else
        {
            if (!rushTime)
            {
                rushTime = true;
                rushPosition = (player.transform.position - transform.position).normalized;
            }
            transform.Translate(rushPosition * mState.speed * Time.deltaTime);
        }

        if (direction.x > 0)
        {
            transform.localScale = new Vector3(monsterX, monsterY, monsterZ);
        }
        else
        {
            transform.localScale = new Vector3(-monsterX, monsterY, monsterZ);
        }
        
    }

    void Hard()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        t += Time.deltaTime;
        if (distance <= 5.0f && hard == false && t >= 7f)
        {
            t = 0;
            hard = true;
            //mState.speed = 0f;
        }
    }


}
