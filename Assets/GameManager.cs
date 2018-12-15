using System.Collections;
using System.Collections.Generic;
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
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;
        winLevelUI.SetActive(true);
    }
}
