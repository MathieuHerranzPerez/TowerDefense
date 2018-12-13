using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isGameEnded = false;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isGameEnded) { }
		else if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    private void EndGame()
    {
        isGameEnded = true;
        Debug.Log("Game Over");

    }
}
