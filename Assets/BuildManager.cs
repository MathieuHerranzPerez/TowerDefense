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

    private GameObject turretToBuild;

    public GameObject GetTurretToBuild()
    {
        return this.turretToBuild;
    }

    
    // Use this for initialization
    void Start ()
    {
        turretToBuild = standardTurretPrefab;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
