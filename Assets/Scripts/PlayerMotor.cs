using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    private Rigidbody rigidBody;
    

	// Use this for initialization
	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void Rotate(Vector3 rotation)
    {
        this.rotation = rotation;
    }

    public void RotateCamera(Vector3 cameraRotation)
    {
        this.cameraRotation = cameraRotation;
    }

    public void Jump(Vector3 jumpForce)
    {
        this.jumpForce = jumpForce;
    }


    private void PerformMovement()
    {
        // if we want to move
        if(velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }
        // if want to jump
        if(jumpForce != Vector3.zero)
        {
            rigidBody.AddForce(jumpForce, ForceMode.Impulse);
        }
    }

    private void PerformRotation()
    {
        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
