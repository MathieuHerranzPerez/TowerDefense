﻿using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public Text upgradeCost;
    public Text sellAmount;
    public Text damage;
    public Text range;
    public Text fireRate;
    public Text slowAmount;
    public Button upgradeButton;
    private Node target;
    [SerializeField]
    private PlayerControler playerCtrl;
    [SerializeField]
    private PlayerShoot playerShoot;
    [SerializeField]
    private UpgradeTurretButtonUI upgradeTurretButtonUI;

    // update the UI fields to match with the selected target, and display it
    public void SetTarget(Node target)
    {
        this.target = target;

        transform.position = target.GetBuildPosition();

        if (target.turretBlueprint.upgradedPrefab != null)
        { 
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost.ToString();
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount().ToString();
        damage.text = "<b>Damage :</b> " + target.turretBlueprint.GetDamage();
        range.text = "<b>Range :</b> " + target.turretBlueprint.GetRange();
        fireRate.text = "<b>Fire rate :</b> " + target.turretBlueprint.GetFireRate();
        slowAmount.text = "<b>Slow :</b> " + target.turretBlueprint.GetSlow();


        MouseManager.lockMouse = false;                         // unlock the cursor
        playerCtrl.LockCamera(true);                            // lock the player cam
        playerShoot.isAllowedToShoot = false;                   // disallow the user to shoot
        target.turret.GetComponent<Turret>().DisplayRange();    // displaythe turret range
        ui.SetActive(true);
    }

    public Turret GetTurretFocused()
    {
        return target.turretBlueprint;
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
        target.turret.GetComponent<Turret>().HideRange();
        ui.SetActive(false);
        upgradeTurretButtonUI.Hide();
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.GetInstance().DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        target.isUpgraded = false;
        BuildManager.GetInstance().DeselectNode();
    }
}
