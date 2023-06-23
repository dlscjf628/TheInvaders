using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameStage : MonoBehaviour
{
    public GameObject[] Player;//0 : 전사, 1 : 거너, 2 : 마법사, 3 : 도박사
    InforMation info;
    private void Awake()
    {
        info = GameObject.Find("GameManager").GetComponent<InforMation>();

        for (int i = 0; i < 4; i++)
        {
            if (info.PlayerJob == i)
            {
                GameObject obj = Instantiate(Player[i], new Vector3(0f, 0f, 0f), Quaternion.identity);
                obj.name = obj.name.Replace("(Clone)", "");
                //Player[i].SetActive(true);
                break;
            }
        }
    }

}
