using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMove : MonoBehaviour
{
    [SerializeField] private WizardState wizardState;
    [SerializeField] private WizardAttack wizardAttack;
    GameObject player;

    float t;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!Manager.instance.gameOver && wizardAttack.isLive)
        {
            if (t < 1.5f)
            {
                t += Time.deltaTime;
            }
            else
            {
                Move();
            }
        }
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * wizardState.speed * Time.deltaTime);
    } 
   
}
