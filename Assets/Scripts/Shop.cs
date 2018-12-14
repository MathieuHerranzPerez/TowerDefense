using UnityEngine;

public class Shop : MonoBehaviour {

    public TurretBlueprint standartTurret;
    public TurretBlueprint missileLauncher;
    public TurretBlueprint laserBeamer;

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

    public void SelectLasezBeamer()
    {
        buildManager.SetTurretToBuild(laserBeamer);
    }
}
