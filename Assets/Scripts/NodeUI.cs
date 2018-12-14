using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    public GameObject ui;
    public Text upgradeCost;
    public Text sellAmount;
    public Button upgradeButton;
    private Node target;


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


    public void Hide()
    {
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
