using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public float maxhp = 10;

    float MonsterDamage = 0;

    public Image HPBarImage;

    // Start is called before the first frame update
    void Start()
    {
        HPBarImage = GameObject.Find("HealthBarRed").transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            MonsterDamage = collision.gameObject.GetComponent<MonsterState>().damage;
            print(MonsterDamage);
            HitDamage();
        }
    }

    public void HitDamage()
    {
        HPBarImage.fillAmount -= MonsterDamage / maxhp;
    }

}
