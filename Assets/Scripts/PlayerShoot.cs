using UnityEngine;

public class PlayerShoot : MonoBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask shootMask;

    [SerializeField]
    private GameObject gun;
    [SerializeField]
    private GameObject cursorUI;

    private Animator gunAnimator;
    private Animator camAnimator;


    private

	// Use this for initialization
	void Start ()
    {
		if(cam == null)
        {
            Debug.LogError("PlayerShoot : No camera referenced");
            this.enabled = false;
        }

        gunAnimator = gun.GetComponent<Animator>();
        camAnimator = cam.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        if(Input.GetMouseButton(1)) // right click held
        {
            Focus();
        }
        else
        {
            Unfocus();
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

    private void Focus()
    {
        gunAnimator.SetBool("IsFocused", true);
        camAnimator.SetBool("IsFocused", true);
        cursorUI.SetActive(false);
    }

    private void Unfocus()
    {
        gunAnimator.SetBool("IsFocused", false);
        camAnimator.SetBool("IsFocused", false);
        cursorUI.SetActive(true);
    }
}
