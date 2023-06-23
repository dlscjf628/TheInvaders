using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour, Damage
{
    float hp = 10;
    float speed = 7f;

    Rigidbody2D rb;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.Translate(movement * speed * Time.deltaTime);
        t += Time.deltaTime;
    }

    public virtual void OnDamage(float damgage, int mode)
    {
        t = 0;
        hp -= damgage;
        if(mode == 2)
        {
            speed = 2f;
            if (t > 3f)
            {
                speed = 7f;
            }
        }
        else if(mode == 4)
        {
            StartCoroutine(Stun());
        }
        print(hp);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Finish"))
        {
            // 충돌한 오브젝트가 공이면, 플레이어의 속도를 0으로 설정합니다.
            rb.velocity = Vector2.zero;
        }
    }
    IEnumerator Stun()
    {
        speed = 0;
        yield return new WaitForSeconds(1.0f);
        speed = 7f;
    }
}
