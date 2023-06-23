using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLookMonster : MonoBehaviour
{
    //  GameObject newBullet;
    public Transform projectileSpawnPoint;
    public Transform Ak47projectileSpawnPoint;
    public Transform MagicCanvas;
    Transform enemyTransform;
    Transform AllenemyTransform;
    Transform weaponTransform;
    Transform BlackholeTransform;
    private List<Transform> enemyTransforms = new List<Transform>();
    public float projectileSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        // 방향 벡터 계산
        Vector3 direction = closestEnemyTransform.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            enemyTransforms.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            enemyTransforms.Remove(collision.transform);

        }
    }

    private Transform GetClosestEnemyTransform()
    {
        Transform closestEnemyTransform = null;
        float closestDistance = Mathf.Infinity;
        foreach (Transform enemyTransform in enemyTransforms)
        {
            float distance = Vector3.Distance(transform.position, enemyTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemyTransform = enemyTransform;
            }
        }
        return closestEnemyTransform;
    }

}
