using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject coinPre;

    List<GameObject>[] pools;
    List<GameObject> coin;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
        coin = new List<GameObject>();
        coin.Add(Instantiate(coinPre, transform));
        coin[0].SetActive(false);
        DontDestroyOnLoad(gameObject);

    }

    public GameObject Get(int i)
    {
        GameObject select = null;
        foreach (GameObject item in pools[i])
        {

            if (!item.activeSelf)  //��Ȱ��ȭ �� item �̸� ����
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)  //item�� �ƹ��͵� ���ٸ�
        {
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }
        return select;
    }

    public GameObject GetCoin()
    {
        GameObject select = null;

        for (int i = 0; i < coin.Count; i++)
        {
            if (!coin[i].activeSelf)
            {
                select = coin[i];
                select.SetActive(true);
                break;
            }
            if (!select)
            {
                select = Instantiate(coinPre, transform);
                coin.Add(select);
            }
        }

        return select;
    }

}
