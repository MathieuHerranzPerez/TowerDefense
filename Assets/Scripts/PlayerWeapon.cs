using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

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

    public GameObject graphics;
    private GameObject tmpGFX;

    [Header("Weapon upgrade")]
    public WeaponUpgrade[] weaponUpgradeList;

    private int currentUpgradeIndex = -1;

    void Start()
    {
        bullets = maxBullets;
        UpdateGFX();
    }

    public void UpgradeWeapon()
    {
        if (IsAnotherUpgrade())         // to be sure
        {
            ++currentUpgradeIndex;
            this.damage = weaponUpgradeList[currentUpgradeIndex].damage;
            this.fireRate = weaponUpgradeList[currentUpgradeIndex].fireRate;
            this.maxBullets = weaponUpgradeList[currentUpgradeIndex].maxBullets;
            this.graphics = weaponUpgradeList[currentUpgradeIndex].upgradedGFX;
            // replace the previous graphics by the new one
            UpdateGFX();
        } 
    }

    public bool IsAnotherUpgrade()
    {
        return !(currentUpgradeIndex >= weaponUpgradeList.Length - 1);
    }

    public int GetUpgradeCost()
    {
        if (IsAnotherUpgrade())
            return weaponUpgradeList[currentUpgradeIndex + 1].upgradeCost;
        else
            return 0;
    }

    private void UpdateGFX()
    {   
        if(tmpGFX != null)
            Destroy(tmpGFX);
        tmpGFX = (GameObject)Instantiate(graphics, transform.position, transform.rotation);
        tmpGFX.transform.SetParent(this.transform);
    }
}
