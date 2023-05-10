using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn1 : MonoBehaviour
{
    public GameObject[] monsters;
    public GameObject spawnSign;

    float t1, t2, t3;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        t1 += Time.deltaTime;
        t2 += Time.deltaTime;
        t3 += Time.deltaTime;
        if (t1 > 2f)
        {
            t1 = 0;
            StartCoroutine(Spawn1(1));
        }
        if (t2 > 3f)
        {
            t2 = 0;
            StartCoroutine(Spawn2(1));
        }
        if (t3 > 5f)
        {
            t3 = 0;
            StartCoroutine(Spawn3(1));
        }
    }

    IEnumerator Spawn1(int n)
    {
        if (n != 3)
        {
            float x, y;
            x = Random.Range(-7.5f, 7.5f);
            y = Random.Range(-4.5f, 4.5f);
            GameObject sign = Instantiate(spawnSign, new Vector2(x, y), Quaternion.identity);
            StartCoroutine(Spawn1(n + 1));
            yield return new WaitForSeconds(0.7f);
            Destroy(sign);
            Instantiate(monsters[0], new Vector2(x, y), Quaternion.identity);
        }
        
    }

    IEnumerator Spawn2(int n)
    {
        if (n != 2)
        {
            float x, y;
            x = Random.Range(-7.5f, 7.5f);
            y = Random.Range(-4.5f, 4.5f);
            GameObject sign = Instantiate(spawnSign, new Vector2(x, y), Quaternion.identity);
            StartCoroutine(Spawn2(n + 1));
            yield return new WaitForSeconds(0.7f);
            Destroy(sign);
            Instantiate(monsters[1], new Vector2(x, y), Quaternion.identity);
        }

    }

    IEnumerator Spawn3(int n)
    {
        if (n != 2)
        {
            float x, y;
            x = Random.Range(-7.5f, 7.5f);
            y = Random.Range(-4.5f, 4.5f);
            GameObject sign = Instantiate(spawnSign, new Vector2(x, y), Quaternion.identity);
            StartCoroutine(Spawn3(n + 1));
            yield return new WaitForSeconds(0.7f);
            Destroy(sign);
            Instantiate(monsters[Random.Range(2, 5)], new Vector2(x, y), Quaternion.identity);
        }

    }

}
