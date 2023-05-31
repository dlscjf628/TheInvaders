using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageBuff : MonoBehaviour
{
    public Text buffText;
    GameObject player;
    GameObject joystick;
    public GameObject weapon;

    int stage;

    public InforMation info;

    public float moveSpeed;
    public float[] attackSpeed = new float[6];
    bool cnt;

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        //joystick = GameObject.Find("Joystick_Time_Buff");
        //buffText = joystick.transform.GetChild(4).gameObject.GetComponent<Text>();
        //info = GameObject.Find("GameManager").GetComponent<InforMation>();
        //weapon = player.transform.GetChild(0).GetChild(2).gameObject;
    }

    void OnEnable()
    {
        player = GameObject.Find("Player");
        joystick = GameObject.Find("Joystick_Time_Buff");
        buffText = joystick.transform.Find("Buff").GetComponent<Text>();
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        weapon = player.transform.GetChild(0).GetChild(2).gameObject;

        moveSpeed = info.PlayerSpeed;
        for (int i = 0; i < 6; i++)
        {
            attackSpeed[i] = info.WearWeaponspeed[i];
            //info.WearWeaponspeed[i] *= 2.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!cnt)
        {
            cnt = true;
            int n = Random.Range(0, 4);
            if (n == 0)
            {
                MoveMentSpeedIncrease();
            }
            else if (n == 1)
            {
                MoveMentSpeedreduction();
            }
            else if (n == 2)
            {
                AttackSpeedUp();
            }
            else if (n == 3)
            {
                AttackSpeedDown();
            }
        }
    }

    void MoveMentSpeedIncrease()
    {
        
        info.PlayerSpeed *= 2f;
        buffText.text = "이동속도 20% 증가";
    }

    void MoveMentSpeedreduction()
    {
        info.PlayerSpeed *= 0.5f;
        buffText.text = "이동속도 20% 감소";
    }

    void AttackSpeedUp()
    {
        for (int i = 0; i < 6; i++)
        {
            //attackSpeed[i] = weapon.transform.GetChild(i).GetChild(0).gameObject.GetComponent<StopAni>().anispeed;
            //weapon.transform.GetChild(i).GetChild(0).gameObject.GetComponent<StopAni>().anispeed *= 1.1f;
            info.WearWeaponspeed[i] *= 2.0f;
        }

        buffText.text = "공격속도 10% 증가";
    }

    void AttackSpeedDown()
    {
        for (int i = 0; i < 6; i++)
        {
            //weapon.transform.GetChild(i).GetChild(0).gameObject.GetComponent<StopAni>().anispeed *= 0.9f;
            info.WearWeaponspeed[i] *= 0.5f;
        }
        buffText.text = "공격속도 10% 감소";
    }

    void DamegeUp()
    {
        for (int i = 0; i < 6; i++)
        {
            weapon.transform.GetChild(i).GetChild(0).gameObject.GetComponent<StopAni>().damage *= 1.1f;
        }
        buffText.text = "공격력 10% 증가";
    }

    void DamegeDown()
    {
        for (int i = 0; i < 6; i++)
        {
            weapon.transform.GetChild(i).GetChild(0).gameObject.GetComponent<StopAni>().damage *= 0.9f;
        }
        buffText.text = "공격력 10% 감소";
    }
}
