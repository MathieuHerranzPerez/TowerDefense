using UnityEngine;

public class Shop : MonoBehaviour {

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

    public void PurchaseStandartTurret()
    {
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }

    public void PurchaseMissileLauncher()
    {
        buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
    }
}
