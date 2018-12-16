using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public Text roundsText;

    public string menuSceneName = "Scenes/MainMenu";

    public SceneFader sceneFader;

    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    public void Retry()
    {
        Time.timeScale = 1f;                            // unfreeze
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1f;                            // unfreeze
        sceneFader.FadeTo(menuSceneName);
    }
}
