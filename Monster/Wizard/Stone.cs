using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    Animator ani;
    Rigidbody2D stoneRb;
    GameObject wizard;
    public float damage = 10f;
    float y;
    bool b;
    Vector2 v;
    // Start is called before the first frame update
    void Start()
    {
        wizard = GameObject.Find("Wizard(Clone)");
        ani = GetComponent<Animator>();
        stoneRb = GetComponent<Rigidbody2D>();
        y = transform.position.y;
        v = Vector2.down * 8f;
    }

    private void Update()
    {
        if (wizard.transform.GetChild(0).gameObject.GetComponent<WizardState>().hp <= 0)
            Destroy(gameObject);
        stoneRb.velocity = v;
        if(transform.position.y < y - 9 && !b)
        {
            b = true;
            GetComponent<CircleCollider2D>().enabled = true;
            v = Vector2.zero;
            ani.SetTrigger("Attack");
            StartCoroutine(StoneCol());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Damage target = collision.GetComponent<Damage>();
            target.OnDamage(damage, 1);
        }
        else if (collision.tag == "Shadow")
        {
            Destroy(collision.gameObject);
        }
    }

    void StoneDestory()
    {
        Destroy(gameObject);
    }

    IEnumerator StoneCol()
    {
        yield return new WaitForSeconds(0.05f);
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
