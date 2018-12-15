using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask shootMask;

	// Use this for initialization
	void Start ()
    {
		if(cam == null)
        {
            Debug.LogError("PlayerShoot : No camera referenced");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        RaycastHit hit;
        // if we hit something that as the shoot mask
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, weapon.range, shootMask))
        {
            // damage the corresponding enemy
            GameObject enemyHit = hit.collider.gameObject;
            ((Enemy)enemyHit.GetComponent(typeof(Enemy))).TakeDamage(weapon.damage);
        }
    }
}
