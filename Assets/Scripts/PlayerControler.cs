﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(WeaponManager))]
public class PlayerControler : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 4f;
    public Slider sliderSensitivity;
    [SerializeField]
    private float jumpForce = 4f;

    [Header("Interaction")]
    [SerializeField]
    private float interacteRange = 4.5f;
    private bool hasFocus = false;
    [SerializeField]
    private LayerMask interactableNodeMask;
    [SerializeField]
    private LayerMask interactableTurretMask;

    //[SerializeField]
    //private GameObject shopIU;
    //private Shop shop;

    private bool rotationLocked = false;

    private Vector3 rotation;
    private Vector3 cameraRotation;

    private bool isGrounded = false;

    private PlayerMotor motor;
    private BuildManager buildManager;
    private WeaponManagerUI weaponManagerUI;
    private WeaponManager weaponManager;

    private void Awake()
    {
        lookSensitivity = PlayerPrefs.GetFloat("sensitivity", lookSensitivity);
        sliderSensitivity.value = lookSensitivity;
    }

    // Use this for initialization
    void Start ()
    {
        motor = GetComponent<PlayerMotor>();
        buildManager = BuildManager.GetInstance();
        weaponManagerUI = GameObject.FindObjectOfType<WeaponManagerUI>();
        weaponManager = GetComponent<WeaponManager>();
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
        //if (hasFocus && velocity != Vector3.zero)       
        //{
        //    buildManager.DeselectNode();
        //    RemoveFocus();
        //}
        // apply the movement
        motor.Move(velocity);

        // ---- ROTATION ----
        if (!rotationLocked)
        {
            // calculate rotation to turn around
            float yRot = Input.GetAxisRaw("Mouse X");

           /* Vector3 */rotation = new Vector3(0f, yRot, 0f) * lookSensitivity;
            // apply rotation
            motor.Rotate(rotation);
        

        // ---- CAMERA ROTATION ----

            // calculate camera rotation to turn around
            float xRot = Input.GetAxisRaw("Mouse Y");

           /* Vector3 */cameraRotation = new Vector3(xRot, 0f, 0f) * lookSensitivity;
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


        // ---- JUMP ----
        Vector3 _jumpForce = Vector3.zero;
        if(Input.GetButton("Jump") && isGrounded)
        {
            //if (hasFocus)
            //{
            //    buildManager.DeselectNode();
            //    RemoveFocus();
            //}
            _jumpForce = Vector3.up * jumpForce;
        }
        // apply the jump force
        motor.Jump(_jumpForce);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!buildManager.IsUIActive())
            {
                RaycastHit hit;
                // if the player is focusing a node
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interacteRange, interactableNodeMask))
                {
                    GameObject node = hit.transform.gameObject;
                    SetFocus(node);
                    if (hasFocus)
                    {
                        node = hit.transform.gameObject;
                        node.GetComponent<Node>().TryToBuild();
                    }
                }
                // if the player is focusing a turret
                else if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interacteRange, interactableTurretMask))
                {
                    GameObject turret = hit.transform.gameObject;
                    SetFocus(turret);
                    if (hasFocus)
                    {
                        turret = hit.transform.gameObject;
                        Node currentNode = turret.GetComponent<Turret>().GetNode();
                        buildManager.SetNode(currentNode);
                    }
                }
            }
            else
            {
                buildManager.DeselectNode();
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!weaponManagerUI.IsActive())
                weaponManagerUI.SetTarget(weaponManager.GetCurrentWeapon());
            else
                weaponManagerUI.Hide();
        }
    }

    private void SetFocus(GameObject interactable)
    {
        this.hasFocus = (interactable != null);
    }

    public void RemoveFocus()
    {
        this.hasFocus = false;
        LockCamera(false);
    }

    public void LockCamera(bool locked)
    {
        rotationLocked = locked;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
            isGrounded = true;
    }

    public void SetLookSensitivity(float value)
    {
        this.lookSensitivity = value;
    }

    public float GetSensitivity()
    {
        return this.lookSensitivity;
    }

    public void SetSpeed(float value)
    {
        this.speed = value;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public void SetSensitivity()
    {
        lookSensitivity = sliderSensitivity.value;
        PlayerPrefs.SetFloat("sensitivity", lookSensitivity);                 // set in the preferences
    }
}
