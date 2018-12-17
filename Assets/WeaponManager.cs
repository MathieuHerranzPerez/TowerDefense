using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField]
    private string weaponLayerName = "Weapon";

    [SerializeField]
    private Transform weaponHolder;
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

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

    private void EquipWeapon(PlayerWeapon weapon)
    {
        currentWeapon = weapon;

        GameObject weaponIns = (GameObject) Instantiate(weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        weaponIns.transform.SetParent(weaponHolder);
    }
}
