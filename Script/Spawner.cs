using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public SpawnData[] spawnData;
    public SpawnDataSlow[] spawnDataSlow;
    public SpawnDataMutant[] spawnDataMutant;
    public SpawnDataPoison[] spawnDataPoison;
    public SpawnDataViper[] spawnDataViper;

    public GameObject manager;

    int level;
    float t, t1;

    float spawnTime = 1f;
    float realTime;
    float[] spTime = new float[] { 5, 4, 3, 2, 1, 1 };

    void Start()
    {

    }

    void Update()
    {
        if (!Manager.instance.gameOver && manager.GetComponent<InforMation>().Stage % 10 != 0)
        {
            t += Time.deltaTime;
            t1 += Time.deltaTime;

            level = Manager.instance.level / 5;

            realTime = spawnTime - (Manager.instance.level * 0.1f);
            if (realTime < 0.2f)
            {
                realTime = 0.2f;
            }

            if (t > realTime)
            {
                t = 0f;
                SpawnMutant();
            }
            if (t1 > spTime[Manager.instance.level / 5])
            {
                t1 = 0;
                int n = Random.Range(0, 4);
                if (n == 0)
                {
                    SpawnMutant();
                }
                else if (n == 1)
                {
                    Spawn();
                }
                else if (n == 2)
                {
                    SpawnSlow();
                }
                else if (n == 3)
                {
                    SpawnPoison();
                }
                else if (n == 4)
                {
                    SpawnViper();
                }
            }
        }

    }

    void SpawnMutant()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5) + 2;
        if (monsterNum > 12)
            monsterNum = 12;

        float x = Random.Range(-16.0f, 16.0f);
        float y = Random.Range(-4.0f, 4.0f);
        GameObject enermy = Manager.instance.pool.Get(monsterNum);
        enermy.transform.position = new Vector2(x, y);
        enermy.GetComponent<MutantState>().Init(spawnDataMutant[level]);
    }

    void Spawn()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5);
        if (monsterNum > 10)
            monsterNum = 10;

        float x = Random.Range(-16.0f, 16.0f);
        float y = Random.Range(-4.0f, 4.0f);
        GameObject enermy = Manager.instance.pool.Get(monsterNum);
        enermy.transform.position = new Vector2(x, y);
        enermy.GetComponent<MonsterState>().Init(spawnData[level]);
    }

    void SpawnSlow()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5) + 1;
        if (monsterNum > 11)
            monsterNum = 11;

        float x = Random.Range(-16.0f, 16.0f);
        float y = Random.Range(-4.0f, 4.0f);
        GameObject enermy = Manager.instance.pool.Get(monsterNum);
        enermy.transform.position = new Vector2(x, y);
        enermy.GetComponent<SlowState>().Init(spawnDataSlow[level]);
    }

    void SpawnPoison()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5) + 3;
        if (monsterNum > 13)
            monsterNum = 13;

        float x = Random.Range(-16.0f, 16.0f);
        float y = Random.Range(-4.0f, 4.0f);
        GameObject enermy = Manager.instance.pool.Get(monsterNum);
        enermy.transform.position = new Vector2(x, y);
        enermy.GetComponent<PoisonState>().Init(spawnDataPoison[level]);
    }

    void SpawnViper()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5) + 4;
        if (monsterNum > 14)
            monsterNum = 14;

        float x = Random.Range(-16.0f, 16.0f);
        float y = Random.Range(-4.0f, 4.0f);
        GameObject enermy = Manager.instance.pool.Get(monsterNum);
        enermy.transform.position = new Vector2(x, y);
        enermy.GetComponent<ViperState>().Init(spawnDataViper[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float health;
    public float speed;
    public float damage;
}


[System.Serializable]
public class SpawnDataSlow
{
    public float health;
    public float speed;
    public float damage;
}

[System.Serializable]
public class SpawnDataMutant
{
    public float health;
    public float speed;
    public float damage;
}

[System.Serializable]
public class SpawnDataPoison
{
    public float health;
    public float speed;
    public float damage;
}

[System.Serializable]
public class SpawnDataViper
{
    public float health;
    public float speed;
    public float damage;
}

