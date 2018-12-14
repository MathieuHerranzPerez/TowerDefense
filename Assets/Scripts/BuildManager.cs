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
    public NodeUI nodeUI;

    private TurretBlueprint turretToBuild;
    private Node selectedNode;


    public void SetNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuild(TurretBlueprint turretBlueprint)
    {
        turretToBuild = turretBlueprint;

        DeselectNode();
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

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }
}
