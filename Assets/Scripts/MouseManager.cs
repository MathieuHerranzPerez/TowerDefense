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
            Debug.Log("NEED TO LOCK : " + lockMouse); // affD
            Debug.Log("IS MOUSE LOCKED : " + isMouseLocked); // affD

            Cursor.lockState = CursorLockMode.Locked;       // lock the cursor
            Cursor.visible = false;
            isMouseLocked = true;
            Debug.Log("--- IS MOUSE LOCKED : " + isMouseLocked); // affD
        }
        else if(!lockMouse && isMouseLocked)
        {
            Debug.Log("NEED TO LOCK : " + lockMouse); // affD
            Debug.Log("IS MOUSE LOCKED : " + isMouseLocked); // affD

            Cursor.lockState = CursorLockMode.None;         // unlock the cursor
            Cursor.visible = true;
            isMouseLocked = false;
            Debug.Log("--- IS MOUSE LOCKED : " + isMouseLocked); // affD
        }
	}
}
