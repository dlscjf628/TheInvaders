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
    float t, t1, t2, t3;
    public int cnt;

    float[] spawnTime = new float[] { 3f, 5f, 6f, 10f };
    float realTime;

    

    void Start()
    {
        t = 5;
    }

    void Update()
    {
        if (!Manager.instance.gameOver && manager.GetComponent<InforMation>().Stage % 10 != 0)
        {
            t += Time.deltaTime;
            t1 += Time.deltaTime;
            t2 += Time.deltaTime;
            t3 += Time.deltaTime;
            
            level = level = Manager.instance.level / 5; ;

            if (t > spawnTime[0] && cnt <= 36)
            {
                t = 0f;
                for (int i = 0; i < 4; i++)
                {
                    SpawnMutant();
                    cnt++;
                }
            }

            if (t1 > spawnTime[1] && cnt <= 36)
            {
                t1 = 0;
                for (int i = 0; i < 2; i++)
                {
                    Spawn();
                    cnt++;
                }

            }

            if (t2 > spawnTime[2] && cnt <= 36)
            {
                t2 = 0;
               
                SpawnSlow();
                cnt++;
                
                SpawnPoison();
                cnt++;

            }
            if (t3 > spawnTime[3] && cnt <= 36)
            {
                t3 = 0;
                
                SpawnViper();
                cnt++;
            }

        }

    }

    void SpawnMutant()
    {
        int monsterNum = ((Manager.instance.level / 5) * 5) + 2;
        if (monsterNum > 12)
            monsterNum = 12;

        float x = Random.Range(-15.0f, 15.0f);
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

        float x = Random.Range(-15.0f, 15.0f);
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

        float x = Random.Range(-15.0f, 15.0f);
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

        float x = Random.Range(-15.0f, 15.0f);
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

        float x = Random.Range(-15.0f, 15.0f);
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

