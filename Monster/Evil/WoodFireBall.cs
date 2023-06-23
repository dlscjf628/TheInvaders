using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFireBall : MonoBehaviour
{
    public GameObject fireBallPre;

    GameObject player;
    GameObject evil;
    float speed = 5f;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        evil = GameObject.Find("EvilGod(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        if (evil.transform.GetChild(0).gameObject.GetComponent<EvilGodState>().hp <= 0)
            Destroy(gameObject);
        if (t > 2f)
        {
            t = 0;

            GameObject fireball = Instantiate(fireBallPre, transform.position, Quaternion.identity);

            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

            Rigidbody2D rb2d = fireball.GetComponent<Rigidbody2D>();
            rb2d.velocity = direction * speed;

        }
    }
}
