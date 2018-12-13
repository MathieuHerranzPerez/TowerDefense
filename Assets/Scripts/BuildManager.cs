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

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return this.turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret)
    {
        this.turretToBuild = turret;
    }

    
    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
