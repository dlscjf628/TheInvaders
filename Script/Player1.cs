using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour, Damage
{
    float hp = 10;
    float speed = 7f;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        
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
        print(hp);
    }

}
