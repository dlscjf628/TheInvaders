 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAni : MonoBehaviour
{
    Animator ani;
    private Rigidbody2D rb;
    private SpriteRenderer sp;

    [SerializeField] private GameObject BulletSp;
    [SerializeField] private GameObject MeteoObj;
    [SerializeField] private GameObject BlackHoleObj;
    [SerializeField] private GameObject HolyProj;
    [SerializeField] private GameObject Lightning;
    private GameObject InsMeteo;
    private GameObject InsLightning;

    private Transform projectileSpawnPoint;
    private Transform Ak47projectileSpawnPoint;
    private Transform enemyTransform;
    private Transform AllenemyTransform;
    private Transform weaponTransform;
    private Transform BlackholeTransform;

    [SerializeField] private float spreadAngle = 30f;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] private float blackHolespeed = 1.5f;
    public float anispeed = 1; //공속
    public float realanispeed;
    public float damage = 10f;// 데미지
    public float realdamage;
    //public float AttackSpeed = 1f;//공속

    public int bulletCount = 3; // 샷건 발사 개수.
    private float angle;


    [SerializeField] private bool Bool_BlackHoleObj = false;
    private bool BlackHoleEffect = false;


    private List<Transform> enemyTransforms = new List<Transform>();


    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        
        damage = gameObject.GetComponent<WeaponManager>().inforealdamage;
        anispeed = gameObject.GetComponent<WeaponManager>().inforealanispeed;

        realanispeed = anispeed;
        realdamage = damage;
        weaponTransform = gameObject.transform.parent;
        projectileSpawnPoint = gameObject.transform.GetChild(0).gameObject.transform;
        Ak47projectileSpawnPoint = gameObject.transform.GetChild(1).gameObject.transform;
            
    }

    // Update is called once per frame
    void Update()
    {
        ani.speed = anispeed;
        if (Bool_BlackHoleObj == true && BlackHoleEffect == true)
        {
            Vector3 direction = transform.position - AllenemyTransform.transform.position;
            AllenemyTransform.transform.Translate(direction.normalized * blackHolespeed * Time.deltaTime);

        }
        if (!Bool_BlackHoleObj)
        {
            gameObject.transform.GetChild(2).GetComponent<BulletDamage>().damage = realdamage;
            //ani.speed = AttackSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            AllenemyTransform = collision.transform;
            
            if (Bool_BlackHoleObj == true)
            {
                if(collision.gameObject.CompareTag("Enemy"))
                    BlackHoleEffect = true;   
                ani.SetTrigger("Attack_Magic");
                Destroy(gameObject, 0.6f);
            }
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (!enemyTransforms.Contains(collision.transform))
            {
                enemyTransforms.Add(collision.transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Boss"))
        {
            if (enemyTransforms.Contains(collision.transform))
            {
                enemyTransforms.Remove(collision.transform);
            }

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

        projectileSpeed = 50f;
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        Vector3 direction = closestEnemyTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.parent.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        
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
       // BulletsniperAndHandgun();
    }
    public void LookMonsterHandGun()
    {
        projectileSpeed = 30f;
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        Vector3 direction = closestEnemyTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + -90f;
        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if (angle < 0f && angle > -180f)
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
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if (angle < 0f && angle > -180f)
        {
            transform.parent.localScale = new Vector3(-0.7f, 0.7f, 1f);
            // newBullet.transform.localScale = new Vector3(0.1f, -0.1f, 1f);
        }
        else
        {
            transform.parent.localScale = new Vector3(0.7f, 0.7f, 1f);
            //  newBullet.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        }
        BulletShotgun();
    }
    public void LookMonsterAk_47()
    {
        Transform closestEnemyTransform = GetClosestEnemyTransform();
            if (closestEnemyTransform == null)
            {
                return;
            }
            projectileSpeed = 30f;

            Vector3 direction = closestEnemyTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.parent.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

            if (angle > -90f && angle < 90f)
            {
                transform.parent.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
            else
            {
                transform.parent.localScale = new Vector3(0.7f, -0.7f, 1f);
            }
            BulletAk47();
        
    }

    public void StopAnimaton()
    {
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

        BulletSp.GetComponent<BulletDamage>().damage = realdamage;
    }
    public void BulletAk47()
    {

        // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        GameObject newBullet = Instantiate(BulletSp, Ak47projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // 방향 벡터 계산
        Vector3 direction = closestEnemyTransform.position - projectileSpawnPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        newBullet.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * projectileSpeed;

        BulletSp.GetComponent<BulletDamage>().damage = realdamage;
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


        BulletSp.GetComponent<BulletDamage>().damage = realdamage;
    }

    public void MagicMeteor()
    { // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }


        InsMeteo = Instantiate(MeteoObj, gameObject.transform.position, gameObject.transform.rotation);


        
        InsMeteo.transform.position = closestEnemyTransform.position;

        InsMeteo.transform.GetChild(1).transform.GetChild(1).transform.GetChild(1).GetComponent<BulletDamage>().damage = realdamage;
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
    {// 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        GameObject InsBlackHole = Instantiate(BlackHoleObj, gameObject.transform.position, gameObject.transform.rotation);

        BlackholeTransform = InsBlackHole.transform;
        Vector3 direction = closestEnemyTransform.position - gameObject.transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;
        InsBlackHole.transform.rotation = Quaternion.AngleAxis(angle - 180f, Vector3.forward);

        Rigidbody2D rig = InsBlackHole.GetComponent<Rigidbody2D>();
        rig.velocity = direction.normalized * projectileSpeed;

        InsBlackHole.transform.GetChild(1).transform.GetChild(0).GetComponent<BulletDamage>().damage = realdamage;
        Destroy(InsBlackHole, 10f);
    }
    public void MagicBlackHoleAni2()
    {
        
        rb.velocity = Vector2.zero;
    }


    public void DestroyObj()
    {
        Destroy(gameObject);
    }

    public void MagicHolly()
    {
        // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        GameObject HolyBullet = Instantiate(HolyProj, gameObject.transform.position, gameObject.transform.rotation);

        HolyBullet.transform.GetChild(0).transform.GetChild(0).GetComponent<BulletDamage>().damage = realdamage;
        

        Destroy(HolyBullet, 2f);
    }
    public void MagicLightning()
    { // 가장 가까운 적의 transform 구하기
        Transform closestEnemyTransform = GetClosestEnemyTransform();
        if (closestEnemyTransform == null)
        {
            return;
        }

        InsLightning = Instantiate(Lightning, gameObject.transform.position , gameObject.transform.rotation);
        
        InsLightning.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        InsLightning.transform.position = closestEnemyTransform.position + new Vector3(0, 3f, 0f);

        InsLightning.transform.GetChild(1).transform.GetChild(0).GetComponent<BulletDamage>().damage = realdamage;
        
    }


}
