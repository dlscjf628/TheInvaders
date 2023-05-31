using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Viper : MonoBehaviour, Damage
{
    MonsterState mState;
    MeleeMonster melee;

    float t = 1f;
    // Start is called before the first frame update
    void Start()
    {
        mState = GetComponent<MonsterState>();
        melee = GetComponent<MeleeMonster>();
        //Physics2D.IgnoreCollision(collA, collB);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnDamage(float damgage, int mode)
    {
        if (melee.hard)
        {
            mState.hp -= damgage / 2;
        }
        else
        {
            mState.hp -= damgage;
        }

        if (mState.hp <= 0)
            gameObject.SetActive(false);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t += Time.deltaTime;
            if (t > 0.5f)
            {
                t = 0f;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(mState.damage, 1);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t = 1.0f;
        }
    }
}
