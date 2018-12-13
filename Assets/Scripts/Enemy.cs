using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range(1f, 50f)]
    public float speed = 10f;


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

    private void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.pointArray.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            targetWaypoint = Waypoints.pointArray[++wavepointIndex];
        }
    }
}
