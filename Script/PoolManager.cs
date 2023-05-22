using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i=0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        
    }

    public GameObject Get(int i)
    {
        GameObject select = null;

        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)  //비활성화 된 item 이면 실행
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)  //item에 아무것도 없다면
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }

}
