using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject ui;

    public string menuSceneName = "Scenes/MainMenu";
    public SceneFader sceneFader;

    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
            MouseManager.lockMouse = false;                 // unlock the cursor
            Time.timeScale = 0f;                            // freeze the game      
        }
        else
        {
            MouseManager.lockMouse = true;                  // lock the cursor 
            Time.timeScale = 1f;                            // unfreeze
        }
    }

    public void Retry()
    {
        Toggle();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Toggle();
        MouseManager.lockMouse = false;                 // unlock the cursor
        sceneFader.FadeTo(menuSceneName);
    }
}
