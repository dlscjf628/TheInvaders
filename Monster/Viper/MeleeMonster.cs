using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonster : MonoBehaviour, Damage
{
    MonsterState mState;
    ViperHard viper;

    GameObject player;

    Vector3 rushPosition;

    Rigidbody2D rigid;
    Collider2D coll;

    bool isLive;

    public bool hard;
    public bool rushTime;

    float t = 7f;

    float monsterX, monsterY, monsterZ;
    // Start is called before the first frame update
    void Awake()
    {
        mState = GetComponent<MonsterState>();
        viper = GetComponent<ViperHard>();
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
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
            transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(monsterX), monsterY, monsterZ);
        }

    }

    public virtual void OnDamage(float damgage, int mode)
    {
        mState.hp -= damgage;
        if (mState.hp <= 0)
        {
            isLive = false;
            rigid.simulated = false;
            coll.enabled = false;
            GameObject coin = Manager.instance.pool.GetCoin();
            coin.transform.position = transform.position;
            gameObject.SetActive(false);
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

    void OnEnable()
    {

        mState = GetComponent<MonsterState>();
        viper = GetComponent<ViperHard>();
        player = GameObject.Find("Player");
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        monsterX = transform.localScale.x;
        monsterY = transform.localScale.y;
        monsterZ = transform.localScale.z;

        isLive = true;
        rigid.simulated = true;
        coll.enabled = true;

        transform.localScale = new Vector3(Mathf.Abs(monsterX), monsterY, monsterZ);


    }

}
