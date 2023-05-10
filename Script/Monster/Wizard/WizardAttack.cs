using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : MonoBehaviour
{
    [SerializeField] private WizardState wizardState;
    public GameObject skullPre;
    public GameObject PoisonskullPre;
    public GameObject shadowStonePre;
    public GameObject stonePre;

    Animator staffAni;

    float t, t1;
    public bool attackTrue;

    float shakeTime = 1.0f;
    float shakeIntensity = 0.01f;

    int skullCount = 5;
    int stoneCount = 10;

    // Start is called before the first frame update
    void Start()
    {
        staffAni = transform.GetChild(4).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
        t += Time.deltaTime;
        if(t > wizardState.attackTime && !attackTrue)
        {
            t = 0;
            int randNum;
            randNum = Random.Range(1, 11);
            //randNum = 8;
            if(randNum >= 1 && randNum <= 4)
            {
                SkullAttack();
            }
            else if(randNum >= 5 && randNum <= 7)
            {
                PoisonSkullAttack();
            }
            else if(randNum >=8 && randNum <= 10)
            {
                StoneAttack();
            }
            else if(randNum >=11 && randNum <= 12)
            {

            }
        }
    }

    void SkullAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff2", true);
        StartCoroutine(CoSkullAttack());
    }

    void PoisonSkullAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff2", true);
        StartCoroutine(CoPoisonSkullAttack());
    }

    void StoneAttack()
    {
        attackTrue = true;
        wizardState.speed = 0f;
        staffAni.SetBool("Staff1", true);
        StartCoroutine(CoStoneAttack());
    }


    void Shake()
    {
        Vector3 originalPosition = transform.position;
        Vector3 shakePosition = originalPosition + Random.insideUnitSphere * shakeIntensity;
        transform.position = shakePosition;
        shakeTime -= Time.deltaTime;
    }
    IEnumerator CoSkullAttack()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < skullCount; i++)
        {
            GameObject fireball = Instantiate(skullPre, transform.position, Quaternion.identity);
        }
        attackTrue = false;
        staffAni.SetBool("Staff2", false);
        wizardState.speed = 4f;
    }
    IEnumerator CoPoisonSkullAttack()
    {
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < skullCount; i++)
        {
            GameObject fireball = Instantiate(PoisonskullPre, transform.position, Quaternion.identity);
        }
        attackTrue = false;
        staffAni.SetBool("Staff2", false);
        wizardState.speed = 4f;
    }

    IEnumerator CoStoneAttack()
    {
        for (int i = 0; i < stoneCount; i++)
        {
            Vector2 randPosition = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4.5f, 4.5f));
            Instantiate(shadowStonePre, randPosition, Quaternion.identity);
            Instantiate(stonePre, new Vector2(randPosition.x, randPosition.y + 9), Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
        }
        attackTrue = false;
        wizardState.speed = 4f;
        staffAni.SetBool("Staff1", false);
        t = 0f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            t1 += Time.deltaTime;
            if (t1 > 0.5f)
            {
                t1 = 0;
                Damage target = collision.GetComponent<Damage>();
                target.OnDamage(wizardState.damage, 1);
            }
        }
    }

}
