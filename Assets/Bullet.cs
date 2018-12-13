using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 70f;
    public GameObject impactEffect;

    private Transform target;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            // if we hit the target
            if(direction.magnitude <= distanceThisFrame)
            {
                HitTarget();
            }
            else
            {
                transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            }
        }
	}

    private void HitTarget()
    {
        // instantiate particules
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 2f);

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
