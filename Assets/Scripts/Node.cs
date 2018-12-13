using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;
    private Renderer rend;
    private Color initialColor;

    BuildManager buildManager;

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

        if(buildManager.GetTurretToBuild() == null)
            return;
        // change the material color
        rend.material.color = hoverColor;
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

        if (buildManager.GetTurretToBuild() == null)
            return;

        if(turret != null)
        {
            Debug.Log("Can't build there");     // affD
        }
        else
        {
            // Build a turret
            GameObject turretToBuild = BuildManager.GetInstance().GetTurretToBuild();
            turret = (GameObject) Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
        }
    }
}
