using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string levelToLoadName = "MainLevel";
    public string tutoName = "Tuto";

    public GameObject CreditsCanvas;

    public SceneFader sceneFader;

	// Use this for initialization
	void Start ()
    {
        FindObjectOfType<AudioManager>().Play("forestAmbience");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HideCredits();
        }
    }

    public void Play()
    {
        MouseManager.lockMouse = true;                  // lock cursor
        sceneFader.FadeTo("Scenes/" + levelToLoadName);
    }

    public void PlayTuto()
    {
        MouseManager.lockMouse = true;                  // lock cursor
        sceneFader.FadeTo("Scenes/" + tutoName);
    }

    public void Quit()
    {
        Debug.Log("Exiting ...");
        Application.Quit();
    }

    public void Credits()
    {
        CreditsCanvas.SetActive(true);
    }

    public void HideCredits()
    {
        CreditsCanvas.SetActive(false);
    }
}
