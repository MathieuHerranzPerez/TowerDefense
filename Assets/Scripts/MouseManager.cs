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
        }
        else if(!lockMouse && isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.None;         // unlock the cursor
            isMouseLocked = false;
        }
	}
}
