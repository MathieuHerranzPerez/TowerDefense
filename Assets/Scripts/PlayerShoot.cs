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

    private AudioManager audioManager;

    private bool isGunFocused = false;

	// Use this for initialization
	void Start ()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (cam == null)
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
            if (Input.GetButtonDown("Fire1") && !weaponManager.isReloading)
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && !weaponManager.isReloading)
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

        if(Input.GetKeyDown("r"))
        {
            TryToReload();
        }

        if(currentWeapon.bullets <= 0)
        {
            TryToReload();
        }            
    }

    private void Shoot()
    {
        if (currentWeapon.bullets <= 0 && isGunFocused)
        {
            audioManager.Play(weaponManager.GetCurrentWeapon().soundClic);    // play the sound  
        }
        else
        {
            --currentWeapon.bullets;

            DoShootEffect();
            // if we hit something that as the shoot mask
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, currentWeapon.range, shootMask))
            {
                // damage the corresponding enemy
                GameObject enemyHit = hit.collider.gameObject;
                Enemy enemy = (Enemy)enemyHit.GetComponent(typeof(Enemy));
                enemy.TakeDamage(currentWeapon.damage, hit.point, hit.normal);
            }
        }
    }

    private void TryToReload()
    {
        // if the gun isn't focus and the player doesn't try to shoot
        if (!isGunFocused)
        {
            audioManager.Play(weaponManager.GetCurrentWeapon().soundReload);    // play the sound
            weaponManager.Reload();
        }
    }

    private void DoShootEffect()
    {
        weaponManager.GetCurrentGFX().muzzleFlash.Play();                 // run the shoot animation
        audioManager.Play(weaponManager.GetCurrentWeapon().soundName);    // play the sound  
    }

    private void DoHitEffect(Vector3 pos, Vector3 normal)
    {
        GameObject effect = (GameObject) Instantiate(weaponManager.GetCurrentGFX().hitEffectPrefab, pos, Quaternion.LookRotation(normal));
        Destroy(effect, 1.5f);
    }

    private void Focus()
    {
        isGunFocused = true;
        gunAnimator.SetBool("IsFocused", true);
        camAnimator.SetBool("IsFocused", true);
        cursorUI.SetActive(false);
    }

    private void Unfocus()
    {
        isGunFocused = false;
        gunAnimator.SetBool("IsFocused", false);
        camAnimator.SetBool("IsFocused", false);
        cursorUI.SetActive(true);
    }
}
