using UnityEngine;

public class BuildManager : MonoBehaviour {

    private static BuildManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More that one BuildManager");
        }
        else
        {
            instance = this;
        }
    }
    public static BuildManager GetInstance()
    {
        return instance;
    }

    public GameObject buildEffect;

    private TurretBlueprint turretToBuild;

    public void SetTurretToBuild(TurretBlueprint turretBlueprint)
    {
        turretToBuild = turretBlueprint;
    }

    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    // check if we the turret is not null
    public bool CanBuild()
    {
        return turretToBuild != null;
    }

    // check if we have enought money to build the selected turret
    public bool HasMoney()
    {
        return PlayerStats.Money >= turretToBuild.cost;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enought money to build it");
        }
        else
        {
            PlayerStats.Money -= turretToBuild.cost;

            // Build the turret
            GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);
            node.turret = turret;
            // effect animation on spawn
            GameObject effect = Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);
        }
    }
}
