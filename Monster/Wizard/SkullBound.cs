using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullBound : MonoBehaviour
{
    Rigidbody2D rb;

    GameObject wizard;

    Vector2 reflected = new Vector2(5f, 3f);

    //public float damage = 100f;
    public float skullSpeed = 8f;
    private void Start()
    {
        wizard = GameObject.Find("Wizard(Clone)");
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
        float currentAngle = Random.Range(0, 360);
        Vector3 direction = Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right;
        reflected = direction * skullSpeed;
    }

    void Update()
    {
        if (wizard.transform.GetChild(0).gameObject.GetComponent<WizardState>().hp <= 0)
            Destroy(gameObject);
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