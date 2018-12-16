using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]     // get the rigidbody too
public class PlayerControler : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpForce = 4f;

    [Header("Interaction")]
    [SerializeField]
    private float interacteRange = 4.5f;
    private Interactable focus;
    [SerializeField]
    private LayerMask interactableMask;

    [SerializeField]
    private GameObject shopIU;
    private Shop shop;

    private bool rotationLocked = false;
    private bool isGrounded = true;

    private PlayerMotor motor;

	// Use this for initialization
	void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        shop = shopIU.GetComponent<Shop>();
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
        // if we move, reset the focus and hide the shop
        if (focus != null && velocity != Vector3.zero)       
        {
            shop.Hide();
            RemoveFocus();
        }
        // apply the movement
        motor.Move(velocity);

        // ---- ROTATION ----
        if (!rotationLocked)
        {
            // calculate rotation to turn around
            float yRot = Input.GetAxisRaw("Mouse X");

            Vector3 rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
            // apply rotation
            motor.Rotate(rotation);
        }

        // ---- CAMERA ROTATION ----
        if (!rotationLocked)
        {
            // calculate camera rotation to turn around
            float xRot = Input.GetAxisRaw("Mouse Y");

            Vector3 cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;
            // apply camera rotation
            motor.RotateCamera(cameraRotation);
        }


        // ---- JUMP ----
        Vector3 _jumpForce = Vector3.zero;
        if(Input.GetButton("Jump") && isGrounded)
        {
            if (focus != null)
            {
                shop.Hide();                                 // hide the shop
                RemoveFocus();
            }
            _jumpForce = Vector3.up * jumpForce;
            isGrounded = false;
        }
        // apply the jump force
        motor.Jump(_jumpForce);


        // to see the target object
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interacteRange, interactableMask))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            SetFocus(interactable);
            if (Input.GetKeyDown("e") && interactable != null)
            {
                SetFocus(interactable);
                shop.Display();                                 // display the shop
                LockCamera(true);                               // lock the cam rotation
                Debug.Log("E --> Raycast : " + interactable);   // affD
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void SetFocus(Interactable focus)
    {
        this.focus = focus;
    }

    public void RemoveFocus()
    {
        this.focus = null;
        LockCamera(false);
    }

    private void LockCamera(bool locked)
    {
        rotationLocked = locked;
    }
}
