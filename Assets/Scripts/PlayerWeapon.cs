using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    public string name = "basic";

    public int damage = 10;
    public float range = 100f;

    public float fireRate = 0f;

    public int maxBullets = 20;
    [HideInInspector]
    public int bullets;

    public float reloadTime = 1f;

    public string soundName = "basicGunShoot";
    public string soundClic = "clicGun";
    public string soundReload = "reload";

    public PlayerWeapon updatedWeapon;

    public PlayerWeapon()
    {
        bullets = maxBullets;
    }
}
