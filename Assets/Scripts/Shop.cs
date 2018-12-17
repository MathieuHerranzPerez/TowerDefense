using UnityEngine;


// deprecated ?
public class Shop : MonoBehaviour {

    [SerializeField]
    private GameObject shopIU;

    public Turret standartTurret;
    public Turret missileLauncher;
    public Turret laserBeamer;


    BuildManager buildManager;

	// Use this for initialization
	void Start ()
    {
        buildManager = BuildManager.GetInstance();
	}

    // Update is called once per frame
    void Update()
    {

    }

    public void Display()
    {
        if (GameManager.GameIsOver)
            return;
        shopIU.SetActive(true);
        MouseManager.lockMouse = false;                 // unlock the cursor   
    }

    public void Hide()
    {
        if (GameManager.GameIsOver)
            return;
        shopIU.SetActive(false);
        MouseManager.lockMouse = true;                  // lock the cursor 
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
