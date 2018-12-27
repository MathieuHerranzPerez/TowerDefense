using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 jumpForce = Vector3.zero;
    private Rigidbody playerRigidbody;
    

	// Use this for initialization
	void Start ()
    {
        playerRigidbody = GetComponent<Rigidbody>();
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
            playerRigidbody.MovePosition(playerRigidbody.position + velocity * Time.fixedDeltaTime);
        }
        // if want to jump
        if(jumpForce != Vector3.zero)
        {
            Vector3 currentForce = new Vector3(0f, -playerRigidbody.velocity.y, 0f);
            currentForce += jumpForce;
            playerRigidbody.AddForce(currentForce, ForceMode.Impulse);
        }
    }

    private void PerformRotation()
    {
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
