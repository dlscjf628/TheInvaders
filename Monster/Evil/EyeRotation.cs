using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeRotation : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // "Player" 태그가 붙은 게임 오브젝트를 찾아서 player 변수에 할당
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // player와 해당 게임 오브젝트 간의 벡터 차이 계산
        Vector2 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 140;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
