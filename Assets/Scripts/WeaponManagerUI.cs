using UnityEngine.UI;
using UnityEngine;

public class WeaponManagerUI : MonoBehaviour {

    [SerializeField]
    private GameObject ui;
    [SerializeField]
    private Text damages;
    [SerializeField]
    private Text fireRate;
    [SerializeField]
    private Text bullets;
    [SerializeField]
    private Button upgradeButton;
    [SerializeField]
    private Text upgradeCost;

    private PlayerWeapon target;

    [SerializeField]
    private PlayerControler playerCtrl;
    [SerializeField]
    private PlayerShoot playerShoot;

    [SerializeField]
    private WeaponManager weaponManager;

    [SerializeField]
    private UpgradeWeaponButtonUI upgradeWeaponButtonUI;


    public void SetTarget(PlayerWeapon weapon)
    {
        this.target = weapon;

        if (target.IsAnotherUpgrade())
        {
            upgradeCost.text = "$" + target.GetUpgradeCost().ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        damages.text = "<b>Damages :</b> " + target.damage;
        fireRate.text = "<b>Fire rate :</b> " + target.fireRate;
        bullets.text = "<b>Bullets :</b> " + target.maxBullets;

        MouseManager.lockMouse = false;         // unlock the cursor
        playerCtrl.LockCamera(true);            // lock the player cam
        playerShoot.isAllowedToShoot = false;   // disallow the user to shoot
        ui.SetActive(true);
    }

    public bool IsActive()
    {
        return ui.activeSelf;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // desactivate the UI
    public void Hide()
    {
        MouseManager.lockMouse = true;      // lock the cursor
        playerCtrl.RemoveFocus();
        playerShoot.isAllowedToShoot = true;
        ui.SetActive(false);
        upgradeWeaponButtonUI.Hide();
    }

    public void Upgrade()
    {
        int upgradeCost = weaponManager.GetCurrentWeapon().GetUpgradeCost();
        if (PlayerStats.Money < upgradeCost)
        {
            Debug.Log("Not enought money to upgrade it");
        }
        else
        {
            weaponManager.GetCurrentWeapon().UpgradeWeapon();
            PlayerStats.Money -= upgradeCost;
            SetTarget(target);      // refresh
        } 
    }

    public void EquipeWeapon()
    {
        // TODO
    }

    public WeaponManager GetWeaponManager()
    {
        return weaponManager;
    }
}
