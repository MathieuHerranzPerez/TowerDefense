﻿using UnityEngine.UI;
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
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
