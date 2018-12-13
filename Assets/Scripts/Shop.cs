using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;

    BuildManager buildManager;

	// Use this for initialization
	void Start ()
    {
        buildManager = BuildManager.GetInstance();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SelectStandartTurret()
    {
        buildManager.SetTurretToBuild(standartTurret);
    }

    public void SelectMissileLauncher()
    {
        buildManager.SetTurretToBuild(missileLauncher);
    }
}
