using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject winLevelUI;

    // Use this for initialization
    void Start ()
    {
        GameIsOver = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(GameIsOver) { }
		else if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    private void EndGame()
    {
        MouseManager.lockMouse = false;                 // unlock the cursor
        GameIsOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;                            // freeze the game
    }

    public void WinLevel()
    {
        MouseManager.lockMouse = false;                 // unlock the cursor
        GameIsOver = true; 
        winLevelUI.SetActive(true);
        Time.timeScale = 0f;                            // freeze the game
    }
}
