using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(AudioSource))]
public class Turret : MonoBehaviour
{

    [Header("General")]
    public float range = 15f;               // range
    public AudioClip buildSound;            // sound when built
    [Range(0.05f, 1f)]
    public float volumeBuild = 0.5f;
    public AudioClip fireSound;             // sound when shoot
    [Range(0.05f, 1f)]
    public float volumeFire = 0.5f;

    public GameObject SphereRange;

    [Header("If base tower")]
    public GameObject prefab;
    public int cost;

    private AudioSource audioSource;
    private bool isPlayingSound = false;    // the laser turrets

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

    public GameObject upgradedPrefab;
    [HideInInspector]
    public float fireRateUP;
    [HideInInspector]
    public float damageUP;
    [HideInInspector]
    public float rangeUP;
    [HideInInspector]
    public float slowUP;

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
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(buildSound, volumeBuild);
        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        // if there is an upgrade
        if(upgradedPrefab != null)
        {
            Turret turretUP = upgradedPrefab.GetComponent<Turret>();
            fireRateUP = turretUP.fireRate;
            rangeUP = turretUP.range;

            if (useLaser)
            {
                slowUP = turretUP.slowPercent;
                damageUP = turretUP.damageOverTime;
            }
            else
            {
                slowUP = 0f;
                damageUP = turretUP.bulletPrefab.GetComponent<Bullet>().damage;
            }
        }

        Vector3 scale = new Vector3(range * 2, range * 2, range * 2);
        scale.x = scale.x / transform.localScale.x;
        scale.y = scale.y / transform.localScale.y;
        scale.z = scale.z / transform.localScale.z;
        SphereRange.transform.localScale = scale;
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

    public void DisplayRange()
    {
        SphereRange.SetActive(true);
    }

    public void HideRange()
    {
        SphereRange.SetActive(false);
    }


    private void LockOnTarget()
    {
        Vector3 direction = target.position - partToRotate.position;   // direction to the target
        // follow the target
        Quaternion lookRatation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
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
        impactEffect.transform.rotation = Quaternion.LookRotation(direction);
        impactEffect.transform.position = target.position + direction.normalized * (target.localScale.x / 2);
        

        if(!isPlayingSound)
        {
            audioSource.loop = true;
            audioSource.clip = fireSound;
            audioSource.volume = volumeFire;
            audioSource.Play();
            isPlayingSound = true;
        }
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(fireSound, volumeFire);     // play the sound
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
            if(isPlayingSound)
            {
                isPlayingSound = false;
                audioSource.Stop();
            }
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
