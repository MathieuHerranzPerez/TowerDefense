using UnityEngine;

//[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform targetWaypoint;
    private int waypointIndex;

    private float delta = 0.3f;

    private Enemy enemy;

    // Use this for initialization
    void Start()
    {
        enemy = GetComponent<Enemy>();
        if (enemy.speed > 30)
            delta = 0.6f;
        targetWaypoint = Waypoints.pointArray[waypointIndex];
    }
	
	// Update is called once per frame
	void Update ()
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
            EndPath();
        }
        else
        {
            ++waypointIndex;
            targetWaypoint = Waypoints.pointArray[waypointIndex];
        }
    }

    private void EndPath()
    {
        if (enemy.isBoss)
            PlayerStats.Lives -= 20;
        else
            --PlayerStats.Lives;
        PathEnd.GetInstance().PlayExplosion();        // notify the END to play an explosion sound
        Destroy(gameObject);
    }

    public int GetWaypoint()
    {
        return this.waypointIndex;
    }
    public void SetWaypoint(int index)
    {
        if (index >= Waypoints.pointArray.Length)
            this.waypointIndex = Waypoints.pointArray.Length - 1;
        else
            this.waypointIndex = index;
        targetWaypoint = Waypoints.pointArray[waypointIndex];
    }
}
