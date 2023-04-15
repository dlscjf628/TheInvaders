using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAni : MonoBehaviour
{
    Animator ani;

    public GameObject BulletSp;
    public GameObject MeteoObj;
    public GameObject BlackHoleObj;
    GameObject InsMeteo;

    //  GameObject newBullet;
    public Transform projectileSpawnPoint;
    public Transform MagicCanvas;
    Transform enemyTransform;
    Transform weaponTransform;
    
    int bulletCount = 5;
    float angle;
    float Bulletangle;
    public float spreadAngle = 30f;
    public float projectileSpeed = 10f;
    
    private List<Transform> enemyTransforms = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        weaponTransform = gameObject.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemyTransforms.Add(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
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

    public void LookMonter()
    {
        projectileSpeed = 30f;
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        Vector3 direction = closestEnemyTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.parent.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
        print(angle);
        
        if (angle > -90f && angle < 90f)
        {
            transform.parent.localScale = new Vector3(-0.7f, 0.7f, 1f);
            //newBullet.transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
        }
        else
        {
            transform.parent.localScale = new Vector3(0.7f, 0.7f, 1f);
            // newBullet.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }
        BulletsniperAndHandgun();
    }
    public void LookMonsterHandGun()
    {
        projectileSpeed = 10f;
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        Vector3 direction = closestEnemyTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        
        transform.parent.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
        if (angle > 90f && angle < 270f)
        {
            transform.parent.localScale = new Vector3(-0.7f, 0.7f, 1f);
        }
        else
        {
            transform.parent.localScale = new Vector3(0.7f, 0.7f, 1f);
        }

        BulletsniperAndHandgun();
    }

    public void LookMonsterShotGun()
    {
        Transform closestEnemyTransform = GetClosestEnemyTransform(); 
        if (closestEnemyTransform == null)
        {
            return;
        }

        // 방향 벡터 계산
        Vector3 direction = closestEnemyTransform.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        print(angle);

        transform.parent.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
        if (angle > 0f && angle < 180f)
        {
            transform.parent.localScale = new Vector3(0.7f, -0.7f, 1f);
            // newBullet.transform.localScale = new Vector3(0.1f, -0.1f, 1f);
        }
        else
        {
            transform.parent.localScale = new Vector3(0.7f, 0.7f, 1f);
            //  newBullet.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }
        BulletShotgun();
    }
    public void StopAnimaton()
    {
        Destroy(InsMeteo);
        ani.SetTrigger("Idle");

    }
    public void StopAnimatonMagic()
    {
        
        ani.SetTrigger("Idle");

    }
    public void BulletsniperAndHandgun()
    {
        // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        GameObject newBullet = Instantiate(BulletSp, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // 방향 벡터 계산
        Vector3 direction = closestEnemyTransform.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        newBullet.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

    }
    public void BulletShotgun()
    {
        // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        // 방향 벡터 계산
        Vector3 direction = closestEnemyTransform.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 탄알 발사
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject newBullet = Instantiate(BulletSp, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            newBullet.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();


            // 모든 탄알이 같은 각도로 발사되도록 설정
            float bulletAngle = angle + (i * spreadAngle) - ((bulletCount - 1) * spreadAngle * 0.5f);
            rb.velocity = Quaternion.AngleAxis(bulletAngle, Vector3.forward) * Vector3.right * projectileSpeed;
        }
    }
    /*
    public void MagicMeteor()
    {
        gameObject.transform.GetChild(1).position = enemyTransform.position;

    }*/
    public void MagicMeteor()
    { // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        InsMeteo = Instantiate(MeteoObj, gameObject.transform.position, gameObject.transform.rotation);
        InsMeteo.transform.SetParent(MagicCanvas);
        
        InsMeteo.transform.position = closestEnemyTransform.position;
    }

    public void MagicScaleContoroller()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0);
    }

    public void InsMagicScaleContoroller()
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
    }

    public void MagicBlackHole()
    {
        GameObject InsBlackHole = Instantiate(BlackHoleObj, gameObject.transform.position, gameObject.transform.rotation);
        InsBlackHole.transform.SetParent(MagicCanvas);

        Vector3 direction = enemyTransform.position - gameObject.transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        InsBlackHole.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        Rigidbody2D rb = InsBlackHole.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

    }
    public void MagicBlackHoleAni2()
    {
        gameObject.transform.position = gameObject.transform.position;
    }
}
