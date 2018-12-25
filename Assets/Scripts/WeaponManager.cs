using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private GameObject primaryWeapon;

    private PlayerWeapon currentWeapon;
    private WeaponGFX currentGFX;

    public bool isReloading = false;

	// Use this for initialization
	void Start ()
    {
        EquipWeapon(primaryWeapon);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGFX GetCurrentGFX()
    {
        return currentGFX;
    }

    private void EquipWeapon(GameObject weapon)
    {
        currentWeapon = weapon.GetComponent<PlayerWeapon>();

        GameObject weaponIns = (GameObject) Instantiate(weapon, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);

        currentGFX = weaponIns.GetComponent<WeaponGFX>();
        if (currentGFX == null)
            Debug.LogError("No weaponGFX component on the weapon object " + weaponIns.name);
    }

    public void Reload()
    {
        if (!isReloading)
        {
            StartCoroutine(Reload_Coroutine());
        }
    }

    private IEnumerator Reload_Coroutine()
    {
        isReloading = true;
        OnReload();
        yield return new WaitForSeconds(currentWeapon.reloadTime);

        currentWeapon.bullets = currentWeapon.maxBullets;

        isReloading = false;
    }

    private void OnReload()
    {
        Animator anim = weaponHolder.GetComponent<Animator>();
        if(anim != null)
        {
            anim.SetTrigger("Reload");
        }
    }

    public int GetWeaponCurrentAmmo()
    {
        return currentWeapon.bullets;
    }
    public int GetWeaponMaxAmmo()
    {
        return currentWeapon.maxBullets;
    }
}
