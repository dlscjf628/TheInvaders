using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilGodAttack : MonoBehaviour
{
    [SerializeField] private EvilGodState evilState;
    public GameObject fireballPrefab;

    float fireballSpeed = 5f;
    float angleStep = 15f;
    int fireballCount = 24;


    public bool attackTrue;
    bool berserkerMode;

    int randNum;
    float t, t1;

    Vector3 fPosition1, fPosition2;

    // Start is called before the first frame update
    void Start()
    {
        fPosition1 = new Vector3((float)(transform.position.x - 0.2), (float)(transform.position.y - 1f), transform.position.z);
        fPosition2 = new Vector3((float)(transform.position.x + 0.2), (float)(transform.position.y - 1f), transform.position.z);
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t > evilState.attackTime && !attackTrue)
        {
            t = 0;
            
            randNum = Random.Range(1, 10);

            if (randNum >= 1 && randNum <= 5)
            {
                NormalFireBall();
            }
            else if(randNum == 6 || randNum == 7)
            {
                SpFireBallOwn();
            }
            else if(randNum == 8 || randNum == 9)
            {
                SpFireBallTwo();
            }
        }
        if (evilState.berserker && !berserkerMode)
        {
            berserkerMode = true;
            fireballSpeed = 7f;
            angleStep = 12f;
            fireballCount = 30;
        }
    }

    void NormalFireBall()
    {
        attackTrue = true;
        float currentAngle = -90f;
        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right;
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = fireball.GetComponent<Rigidbody2D>();
            rb2d.velocity = direction * fireballSpeed;
            currentAngle += angleStep;
        }
        attackTrue = false;
    }
    void SpFireBallOwn()
    {
        attackTrue = true;
        for (int i = 0; i < 4; i++)
            StartCoroutine(CoSpFireBallOwn(i));
    }

    void SpFireBallTwo()
    {
        attackTrue = true;
        for(int i=0; i<4; i++)
            StartCoroutine(CoSpFireBallTwo(i));
    }


    IEnumerator CoSpFireBallTwo(int a)
    {
        float[] currentAngle = new float[] { 0, 90, 180, 270 };

        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle[a], Vector3.forward) * Vector3.right;
            GameObject fireball1 = Instantiate(fireballPrefab, fPosition1, Quaternion.identity);
            GameObject fireball2 = Instantiate(fireballPrefab, fPosition2, Quaternion.identity);

            Rigidbody2D rb2d1 = fireball1.GetComponent<Rigidbody2D>();
            rb2d1.velocity = direction * fireballSpeed;
            Rigidbody2D rb2d2 = fireball2.GetComponent<Rigidbody2D>();
            rb2d2.velocity = direction * fireballSpeed;
            currentAngle[a] += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
        
        attackTrue = false;
    }

    IEnumerator CoSpFireBallOwn(int a)
    {
        float[] currentAngle = new float[] { 0, 90, 180, 270 };
        for (int i = 0; i < fireballCount; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(currentAngle[a], Vector3.forward) * Vector3.right;
            GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb2d = fireball.GetComponent<Rigidbody2D>();
            rb2d.velocity = direction * fireballSpeed;
            currentAngle[a] += angleStep;
            yield return new WaitForSeconds(0.1f);
        }
        attackTrue = false;
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
                target.OnDamage(evilState.damage, 1);
            }
        }
    }

}
