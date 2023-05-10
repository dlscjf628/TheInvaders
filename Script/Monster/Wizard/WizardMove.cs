using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMove : MonoBehaviour
{
    [SerializeField] private WizardState wizardState;

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
        if (t < 3f)
        {
            t += Time.deltaTime;
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * wizardState.speed * Time.deltaTime);
    } 
   
}
