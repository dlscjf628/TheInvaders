using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour
{
    Vector2 reflected = new Vector2(5f, 3f);

    void Update()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = reflected;
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
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody2D plRb = col.gameObject.GetComponent<Rigidbody2D>();
            plRb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            plRb.constraints = RigidbodyConstraints2D.None;
            Destroy(gameObject);
        }
    }
}