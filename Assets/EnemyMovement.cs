using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform targetWaypoint;
    private int wavepointIndex = 0;

    private Enemy enemy;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Enemy>();
        targetWaypoint = Waypoints.pointArray[0];
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
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
