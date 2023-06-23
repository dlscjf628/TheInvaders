using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWeaponEffect : MonoBehaviour
{
    public GameObject[] AttackEffect;

    public bool Lightning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if(collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (Lightning == true)
            {
                int i = Random.Range(0, AttackEffect.Length);
                GameObject effect = Instantiate(AttackEffect[i], gameObject.transform.position + new Vector3(0, -3f, 0), gameObject.transform.rotation);
            }
            else
            {
                int i = Random.Range(0, AttackEffect.Length);
                GameObject effect = Instantiate(AttackEffect[i], gameObject.transform.position, gameObject.transform.rotation);
            }
        }*/
    }
}
