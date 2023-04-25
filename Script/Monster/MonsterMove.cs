using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    MonsterState mState;

    GameObject player;

    float monsterX, monsterY, monsterZ;
    // Start is called before the first frame update
    void Start()
    {
        mState = GetComponent<MonsterState>();
        player = GameObject.Find("Player");
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance >= 5.0f)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.Translate(direction * mState.speed * Time.deltaTime);

            if (direction.x > 0)
            {
                transform.localScale = new Vector3(monsterX, monsterY, monsterZ);
            }
            else
            {
                transform.localScale = new Vector3(-monsterX, monsterY, monsterZ);
            }
        }
    }
    
    void Die()
    {
        Destroy(gameObject, 1f);
    }

}
