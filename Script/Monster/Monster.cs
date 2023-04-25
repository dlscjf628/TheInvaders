using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : MonoBehaviour, Damage
{
    MonsterState mState;

    float t = 1f;
    // Start is called before the first frame update
    void Start()
    {
        mState = GetComponent<MonsterState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnDamage(float damgage, int mode)
    {
        mState.hp -= damgage;
        if (mState.hp <= 0)
            Die();

    }
    void Die()
    {
        Destroy(gameObject, 1f);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        Damage target = collision.GetComponent<Damage>();
    //        target.OnDamage(mState.damage);
    //        GetComponent<CircleCollider2D>().enabled = false;
    //        StartCoroutine(OnHitTime());
    //    }
    //}

    IEnumerator OnHitTime()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<CircleCollider2D>().enabled = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t += Time.deltaTime;
            if (t > 0.5f)
            {
                t = 0;
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
