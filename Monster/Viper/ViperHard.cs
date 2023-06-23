using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperHard : MonoBehaviour
{
    [SerializeField] private MeleeMonster melee;
    [SerializeField] private ViperState mstate;

    Animator ani;

    bool temp;
    public bool rush;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (melee.hard)
        {
            t += Time.deltaTime;

            if (!temp)
            {
                temp = true;
                ani.SetBool("Hard", true);
                Color newColor = new Color32(4, 160, 219, 255);
                GetComponent<Renderer>().material.color = newColor;
                mstate.speed = 0f;
            }

            if (t > 1.5f)
            {
                t = 0;
                melee.hard = false;
                melee.rushTime = false;
                temp = false;
                rush = false;
                mstate.speed = 3f;
                Color newColor = new Color32(255, 255, 255, 255);
                GetComponent<Renderer>().material.color = newColor;
            }
        }
    }

    void EndHard()
    {
        ani.SetBool("Hard", false);
        mstate.speed = 10f;
        StartCoroutine(rushStart());
    }

    IEnumerator rushStart()
    {
        yield return new WaitForSeconds(0.05f);
        rush = true;
    }
}
