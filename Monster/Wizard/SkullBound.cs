using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBound : MonoBehaviour
{
    Rigidbody2D rb;

    Vector2 reflected = new Vector2(5f, 3f);

    float damage = 2f;
    float skullSpeed = 6f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5f);
        float currentAngle = Random.Range(0, 360);
        Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right;
        reflected = direction * skullSpeed;
    }

    void Update()
    {
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = reflected;
        float angle = Mathf.Atan2(reflected.y, reflected.x) * Mathf.Rad2Deg; // 움직이는 방향의 각도 계산
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); // 오브젝트 회전
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Wall"))
        {
            Rigidbody2D wallRb = col.gameObject.GetComponent<Rigidbody2D>();
            wallRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            Vector2 normal = col.contacts[0].normal;
            reflected = Vector2.Reflect(reflected, normal);
            wallRb.constraints = RigidbodyConstraints2D.None;
            wallRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D plRb = col.gameObject.GetComponent<Rigidbody2D>();
            plRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            plRb.constraints = RigidbodyConstraints2D.None;

        }
    }
}