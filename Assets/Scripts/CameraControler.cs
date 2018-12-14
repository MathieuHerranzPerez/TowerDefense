using UnityEngine;

public class CameraControler : MonoBehaviour {

    public float panSpeed = 30f;
    public float panBorderThickness = 3f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 100f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if the game is over, disable all controls
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
        }
        else
        {
            if (Input.GetKey("z") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey("q") || Input.mousePosition.x <= panBorderThickness)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
            }
            
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            Vector3 pos = transform.position;
            pos.y -= scroll * scrollSpeed * 500 * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;
        }
    }
}
