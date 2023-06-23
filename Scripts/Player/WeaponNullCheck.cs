using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNullCheck : MonoBehaviour
{
    public InforMation info;
    // Start is called before the first frame update
    void Start()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();
        for (int i = 0; i < 6; i++)
        {
            if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<WeaponManager>().ObjectNumber == i)
            {
                if (info.weaponname[i] == 12)
                {
                    gameObject.transform.GetChild(i).gameObject.SetActive(false);
                }
            }


        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}