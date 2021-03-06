﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    //public Color hoverColor;
    //public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public Turret turretBlueprint;

    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    //private Color initialColor;

    public Material materialWhenCanBuild;
    public Material materialWhenNotEnoughMoney;
    private Material initialMaterial;

    BuildManager buildManager;


    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        //initialColor = rend.material.color;
        initialMaterial = rend.material;

        buildManager = BuildManager.GetInstance();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseEnter()
    {
        // desable the Node pointing when clicking on IU
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(!buildManager.CanBuild())
            return;

        if (buildManager.HasMoney())
        {
            // change the material color
            //rend.material.color = hoverColor;
            rend.material = materialWhenCanBuild;
        }
        else
        {
            // change the material color
            //rend.material.color = notEnoughMoneyColor;
            rend.material = materialWhenNotEnoughMoney;
        }
    }


    void OnMouseExit()
    {
        // reset the color
        //rend.material.color = initialColor;
        rend.material = initialMaterial;
    }

    public void TryToBuild()
    {
        if (turret != null)
        {
            buildManager.SetNode(this);
            return;
        }

        if (!buildManager.CanBuild())
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    private void BuildTurret(Turret blueprint)
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            Debug.Log("Not enought money to build it");
        }
        else
        {
            PlayerStats.Money -= blueprint.cost;

            // Build the turret
            GameObject turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            this.turret = turret;
            turret.GetComponent<Turret>().SetNode(this);        // give a reference to this node at the turret

            turretBlueprint = turret.GetComponent<Turret>();

            // effect animation on spawn
            GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
        }
    }

    // replace the current tower by the upgraded turret
    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            Debug.Log("Not enought money to upgrade it");
        }
        else
        {
            PlayerStats.Money -= turretBlueprint.upgradeCost;

            // for the sell option
            int tmpCost = this.turretBlueprint.upgradeCost;

            // destroy the old turret
            Destroy(this.turret);

            // build the new turret
            GameObject turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
            this.turret = turret;
            this.turretBlueprint = turret.GetComponent<Turret>();
            this.turretBlueprint.cost = tmpCost;
            turret.GetComponent<Turret>().SetNode(this);        // give a reference to this node at the turret
            // effect animation on spawn
            GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            isUpgraded = true;
        }
    }

    public void SellTurret()
    {
        // give money to the user
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        // effect animation on delete
        GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        // destroy the turret
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;
    }
}
