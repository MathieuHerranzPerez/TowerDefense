using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class CameraTuto : MonoBehaviour {
    
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float lookSensitivity = 3f;

    private bool rotationLocked = false;

    private PlayerMotor motor;

    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }
		
	// Update is called once per frame
	void Update ()
    {
        // ---- ROTATION ----
        if (!rotationLocked)
        {
            // calculate rotation to turn around
            float yRot = Input.GetAxisRaw("Mouse X");

            Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
            // apply rotation
            motor.Rotate(rotation);


            // ---- CAMERA ROTATION ----

            // calculate camera rotation to turn around
            float xRot = Input.GetAxisRaw("Mouse Y");

            Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;
            // apply camera rotation
            motor.RotateCamera(cameraRotation);
        }
    }
}
