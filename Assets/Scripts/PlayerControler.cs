using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]     // get the rigidbody too
public class PlayerControler : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpForce = 4f;

    private bool isGrounded = true;

    private PlayerMotor motor;

	// Use this for initialization
	void Start ()
    {
        motor = GetComponent<PlayerMotor>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // ---- MOVEMENT ----
        // calculate movement
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMov;    // (1, 0, 0)
        Vector3 moveVertical = transform.forward * zMov;    // (0, 0, 1)

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;
        // apply the movement
        motor.Move(velocity);

        // ---- ROTATION ----
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


        // ---- JUMP ----
        Vector3 _jumpForce = Vector3.zero;
        if(Input.GetButton("Jump") && isGrounded)
        {
            _jumpForce = Vector3.up * jumpForce;
            isGrounded = false;
        }
        // apply the jump force
        motor.Jump(_jumpForce);

    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
