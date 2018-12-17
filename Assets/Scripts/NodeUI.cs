using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;
    private Node target;
    [SerializeField]
    private PlayerControler playerCtrl;

    // update the UI fields to match with the selected target, and display it
    public void SetTarget(Node target)
    {
        this.target = target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
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

        MouseManager.lockMouse = false;         // unlock the cursor
        playerCtrl.LockCamera(true);            // lock the player cam
        ui.SetActive(true);
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
        playerCtrl.LockCamera(false);       // unlock the player cam
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
