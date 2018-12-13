using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    [Header("Attributes")]
    public float range = 15f;               // range
    [Range(0.05f, 20f)]
    public float fireRate = 1f;

    [Header("Setup Fields")]
    public string enemyTag = "Enemy";       // target type
    public Transform partToRotate;          // part of the turret to rotate
    [Range(1f, 30f)]
    public float turnSpeed = 10f;           // smooth when turn

    public GameObject bulletPrefab;
    public Transform firePoint;
          
    private Transform target;
    private float fireCountdown = 0f;

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null)
        {
            Vector3 direction = target.position - transform.position;   // direction to the target

            // follow the target
            Quaternion lookRatation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRatation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);


            if(fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
	}

    private void Shoot()
    {
        GameObject bulletGameObject = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGameObject.GetComponent<Bullet>();

        if(bullet != null)
        {
            bullet.SetTarget(target);
        }
    }

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

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
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
