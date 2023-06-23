using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceManager : MonoBehaviour
{
    Animator ani;
    bool AttackAni; // 몬스터에 닿으면 애니메이션 발동
    SpriteRenderer sp;
    int count = 0;
    public int ak = 2;
    bool check = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        ani = gameObject.transform.GetChild(0).transform.GetComponent<Animator>();
        sp = gameObject.transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
       
        AttackAni = true;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((sp.sprite.name == "Ak47" && collision.gameObject.tag == "Enemy" && check == true) || (sp.sprite.name == "Ak47" && collision.gameObject.tag == "Boss" && check == true))
        {
            if (count < ak)
            {

                print("check : " + count);
                StartCoroutine(AkAttackAnimation());
            }
            else
            {
                check = false;
                StartCoroutine(AttackIdle());
            }

            /*
            if (collision.gameObject.tag == "Enemy" && gameObject.transform.GetComponent<CircleCollider2D>().enabled == true)
            {
                ani.SetTrigger("Idle");
            }
            */
        }
        else if (sp.sprite.name == null  || (sp.sprite.name != "Ak47" && collision.gameObject.tag == "Enemy" && gameObject.transform.GetComponent<CircleCollider2D>().enabled == true) || (sp.sprite.name != "Ak47" && collision.gameObject.tag == "Boss" && gameObject.transform.GetComponent<CircleCollider2D>().enabled == true))
        {
            StartCoroutine(AttackAnimation());
        }
    }
    IEnumerator AttackAnimation()
    {
        ani.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);

    }
    IEnumerator AkAttackAnimation()
    {
        ani.SetTrigger("Attack");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<CircleCollider2D>().enabled = true;

    }
    IEnumerator AttackIdle()
    {
        ani.SetTrigger("Idle");
        gameObject.transform.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.transform.GetComponent<CircleCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(2f); ;
        gameObject.transform.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.transform.GetComponent<CircleCollider2D>().isTrigger = true;
        check = true;
        count = 0;

    }


}
