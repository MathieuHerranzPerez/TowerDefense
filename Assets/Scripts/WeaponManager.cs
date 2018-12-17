using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;
    private WeaponGFX currentGFX;

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

    private void EquipWeapon(PlayerWeapon weapon)
    {
        currentWeapon = weapon;

        GameObject weaponIns = (GameObject) Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);

        currentGFX = weaponIns.GetComponent<WeaponGFX>();
        if (currentGFX == null)
            Debug.LogError("No weaponGFX component on the weapon object " + weaponIns.name);
    }
}
