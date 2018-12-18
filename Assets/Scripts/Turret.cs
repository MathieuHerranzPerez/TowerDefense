using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("General")]
    public float range = 15f;               // range

    [Header("Use Bullets (default)")]
    [Range(0.05f, 20f)]
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    private float fireCountdown = 0f;

    [Header("Use Laser")]
    public bool useLaser = false;
    public int damageOverTime = 30;
    public float slowPercent = 0.5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Setup Fields")]
    public string enemyTag = "Enemy";       // target type
    public Transform partToRotate;          // part of the turret to rotate
    [Range(1f, 30f)]
    public float turnSpeed = 10f;           // smooth when turn

    public Transform firePoint;

    private Transform target;
    private Enemy targetEnemy;


    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    private Node node;


    public void SetNode(Node node)
    {
        this.node = node;
    }

    public int GetSellAmount()
    {
        return (int)(cost / 1.5f);
    }

    public Node GetNode()
    {
        return this.node;
    }

    public float GetRange()
    {
        return this.range;
    }

    public float GetFireRate()
    {
        if (useLaser)
            return 0f;
        else
            return this.fireRate;
    }

    public int GetDamage()
    {
        if (useLaser)
            return this.damageOverTime;
        else
            return bulletPrefab.GetComponent<Bullet>().damage;
    }

    public float GetSlow()
    {
        if (useLaser)
            return this.slowPercent;
        else
            return 0f;
    }

    // Use this for initialization
    void Start()
    {
        prefab = gameObject;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            LockOnTarget();

            if (useLaser)
            {
                Laser();
            }
            else
            {
                if (fireCountdown <= 0)
                {
                    Shoot();
                    fireCountdown = 1f / fireRate;
                }

                fireCountdown -= Time.deltaTime;
            }
        }
        else
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
        }
    }

    public void Upgrade()
    {
        node.UpgradeTurret();
    }

    public void Sell()
    {
        node.SellTurret();
    }


    private void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;   // direction to the target
        // follow the target
        Quaternion lookRatation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercent);

        // graphics

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 direction = firePoint.position - target.position;

        // put the effect on the enemy border
        impactEffect.transform.position = target.position + direction.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Shoot()
    {
        GameObject bulletGameObject = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

    // if we want to change the target strategy, it's here
    private void UpdateTarget()
    {
        GameObject[] enemyArray = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // find the nearest enemy
        foreach (GameObject enemy in enemyArray)
        {
            // get ditance between the enemy and this
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    /**
     *  Show the range in gizmos
     */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
