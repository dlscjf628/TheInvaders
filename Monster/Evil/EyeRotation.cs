using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotation : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // "Player" �±װ� ���� ���� ������Ʈ�� ã�Ƽ� player ������ �Ҵ�
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // player�� �ش� ���� ������Ʈ ���� ���� ���� ���
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 140;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
