using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject ui;

    public string menuSceneName = "Scenes/MainMenu";
    public SceneFader sceneFader;

    [SerializeField]
    private PlayerShoot playerShoot;

    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
	}

    public void Toggle()
    {
        if (GameManager.GameIsOver)
            return;

        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            playerShoot.isAllowedToShoot = false;
            MouseManager.lockMouse = false;                 // unlock the cursor
            Time.timeScale = 0f;                            // freeze the game     
        }
        else
        {
            playerShoot.isAllowedToShoot = true;
            MouseManager.lockMouse = true;                  // lock the cursor 
            Time.timeScale = 1f;                            // unfreeze
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        WaveSpawner.Instance.EnemiesAlive = 0;
    }

    public void Menu()
    {
        Toggle();
        MouseManager.lockMouse = false;                 // unlock the cursor
        sceneFader.FadeTo(menuSceneName);
    }
}
