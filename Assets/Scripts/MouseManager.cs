using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour {

    public static bool lockMouse = false;

    private bool isMouseLocked = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(lockMouse && !isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;       // lock the cursor
            isMouseLocked = true;
            Debug.Log("LOCK");
        }
        else if(!lockMouse && isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Confined;     // unlock the cursor
            isMouseLocked = false;
            Debug.Log("UNLOCK");
        }
	}
}
