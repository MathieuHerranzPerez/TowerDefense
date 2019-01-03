using UnityEngine;
using UnityEngine.UI;

public class PlayerControlerTuto : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float lookSensitivity = 4f;
    public Slider sliderSensitivity;

    private bool rotationLocked = false;

    private Vector3 rotation;
    private Vector3 cameraRotation;

    private PlayerMotor motor;

    private void Awake()
    {
        lookSensitivity = PlayerPrefs.GetFloat("sensitivity", lookSensitivity);
        sliderSensitivity.value = lookSensitivity;
    }

    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        // ---- ROTATION ----
        if (!rotationLocked)
        {
            // calculate rotation to turn around
            float yRot = Input.GetAxisRaw("Mouse X");

            /* Vector3 */
            rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
            // apply rotation
            motor.Rotate(rotation);


            // ---- CAMERA ROTATION ----

            // calculate camera rotation to turn around
            float xRot = Input.GetAxisRaw("Mouse Y");

            /* Vector3 */
            cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;
            // apply camera rotation
            motor.RotateCamera(cameraRotation);
        }
        else
        {
            rotation = new Vector3(0f, 0f, 0f);
            cameraRotation = new Vector3(0f, 0f, 0f);
            motor.Rotate(rotation);
            motor.RotateCamera(cameraRotation);
        }
    }

    public void LockCamera(bool locked)
    {
        rotationLocked = locked;
    }

    public void SetLookSensitivity(float value)
    {
        this.lookSensitivity = value;
    }

    public float GetSensitivity()
    {
        return this.lookSensitivity;
    }

    public void SetSensitivity()
    {
        lookSensitivity = sliderSensitivity.value;
        PlayerPrefs.SetFloat("sensitivity", lookSensitivity);                 // set in the preferences
    }
}
