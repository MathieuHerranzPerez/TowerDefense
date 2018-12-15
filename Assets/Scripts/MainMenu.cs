using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public string levelToLoad = "MainLevel";

    public SceneFader sceneFader;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void Play()
    {
        sceneFader.FadeTo("Scenes/" + levelToLoad);
        // SceneManager.LoadScene("Scenes/" + levelToLoad);
    }

    public void Quit()
    {
        Debug.Log("Exiting ...");
        Application.Quit();
    }
}
