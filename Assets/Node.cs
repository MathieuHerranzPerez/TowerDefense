using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    public Vector3 positionOffset;

    private GameObject turret;
    private Renderer rend;
    private Color initialColor;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        initialColor = rend.material.color;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseEnter()
    {
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
