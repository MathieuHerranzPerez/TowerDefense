using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public Text roundsText;

    public string menuSceneName = "Scenes/MainMenu";

    public SceneFader sceneFader;

    void OnEnable()
    {
        int nbRound = PlayerStats.Rounds - 1;
        roundsText.text = nbRound.ToString();
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
        MouseManager.lockMouse = true;
        Time.timeScale = 1f;                            // unfreeze
        WaveSpawner.Instance.EnemiesAlive = 0;
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        MouseManager.lockMouse = false;
        Time.timeScale = 1f;                            // unfreeze
        sceneFader.FadeTo(menuSceneName);
    }
}
