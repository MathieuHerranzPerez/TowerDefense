using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public Text upgradeCost;
    public Text sellAmount;
    public Text damages;
    public Text range;
    public Text fireRate;
    public Text slowAmount;
    public Button upgradeButton;
    private Node target;
    [SerializeField]
    private PlayerControler playerCtrl;
    [SerializeField]
    private PlayerShoot playerShoot;

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
        damages.text = "<b>Damages :</b> " + target.turretBlueprint.GetDamage();
        range.text = "<b>Range :</b> " + target.turretBlueprint.GetRange();
        fireRate.text = "<b>Fire rate :</b> " + target.turretBlueprint.GetFireRate();
        slowAmount.text = "<b>Slow :</b> " + target.turretBlueprint.GetSlow();


        MouseManager.lockMouse = false;         // unlock the cursor
        playerCtrl.LockCamera(true);            // lock the player cam
        playerShoot.isAllowedToShoot = false;   // disallow the user to shoot
        ui.SetActive(true);
    }

    public bool IsActive()
    {
        return ui.active;
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
