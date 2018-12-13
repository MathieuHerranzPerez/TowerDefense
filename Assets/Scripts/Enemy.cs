using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range(1f, 50f)]
    public float speed = 10f;

    public int health = 100;
    public int value = 50;

    public GameObject deathEffect;


    private Transform targetWaypoint;
    private int wavepointIndex = 0;

	// Use this for initialization
	void Start ()
    {
        targetWaypoint = Waypoints.pointArray[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);

        if(Vector3.Distance(transform.position, targetWaypoint.position) <= 0.3f)
        {
            GetNextWaypoint();
        }
	}

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // add money to the user
        PlayerStats.Money += value;

        // effect on death
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        // destroy the enemy
        Destroy(gameObject);
    }

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.pointArray.Length - 1)
        {
            EndPath();
        }
        else
        {
            targetWaypoint = Waypoints.pointArray[++wavepointIndex];
        }
    }

    private void EndPath()
    {
        --PlayerStats.Lives;
        Destroy(gameObject);
    }
}
