using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour {

    public float range = 15f;
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    [Range(1f, 30f)]
    public float turnSpeed = 10f;

    private Transform target;

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
