using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class EnemyMovementTuto : MonoBehaviour
{

    private Transform targetWaypoint;
    private int waypointIndex;

    private float delta = 0.3f;

    private EnemyTuto enemy;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<EnemyTuto>();
        if (enemy.speed > 20 && enemy.speed <= 30)
            delta = 0.6f;
        else if (enemy.speed > 30)
            delta = 1f;
        targetWaypoint = Waypoints.pointArray[waypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = targetWaypoint.position - transform.position;
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetWaypoint.position) <= delta)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.pointArray.Length - 1)
        {
            waypointIndex = 0;
        }
        else
        {
            ++waypointIndex;
        }
        targetWaypoint = Waypoints.pointArray[waypointIndex];
    }
}
