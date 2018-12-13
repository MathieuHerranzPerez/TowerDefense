using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color initialColor;

    BuildManager buildManager;


    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    // Use this for initialization
    void Start ()
    {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;

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
            rend.material.color = hoverColor;
        }
        else
        {
            // change the material color
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        // reset the color
        rend.material.color = initialColor;
    }

    void OnMouseDown()
    {
        // desable the Node pointing when clicking on IU
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild())
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there");     // affD
        }
        else
        {
            buildManager.BuildTurretOn(this);
        }
    }
}
