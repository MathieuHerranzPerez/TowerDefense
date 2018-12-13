using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;
    public int damage = 50;

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
                transform.LookAt(target);
            }
        }
	}

    private void HitTarget()
    {
        // instantiate particules
        GameObject effectInstance = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInstance, 5f);

        if(explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }

    private void Damage(Transform enemyGameObject)
    {
        Enemy enemy = enemyGameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
    }

    private void Explode()
    {
        // get all what is hit
        Collider[] colliderHitObjectArray = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliderHitObjectArray)
        {
            if(collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
