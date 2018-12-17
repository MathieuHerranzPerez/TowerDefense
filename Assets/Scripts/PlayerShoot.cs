using UnityEngine;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : MonoBehaviour {

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

    private PlayerWeapon currentWeapon;
    private WeaponManager weaponManager;

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

        weaponManager = GetComponent<WeaponManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();

        if(currentWeapon.fireRate <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f / currentWeapon.fireRate);
            }
            else if(Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
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
        Debug.Log("shoot"); // affD
        DoShootEffect();
        RaycastHit hit;
        // if we hit something that as the shoot mask
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, shootMask))
        {
            // damage the corresponding enemy
            GameObject enemyHit = hit.collider.gameObject;
            //((Enemy)enemyHit.GetComponent(typeof(Enemy))).TakeDamage(currentWeapon.damage);
            Enemy enemy = (Enemy) enemyHit.GetComponent(typeof(Enemy));
            enemy.TakeDamage(currentWeapon.damage, hit.point, hit.normal);

            //DoHitEffect(hit.point, hit.normal);
        }
    }

    private void DoShootEffect()
    {
        weaponManager.GetCurrentGFX().muzzleFlash.Play();
    }

    private void DoHitEffect(Vector3 pos, Vector3 normal)
    {
        GameObject effect = (GameObject) Instantiate(weaponManager.GetCurrentGFX().hitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(effect, 1.5f);
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
