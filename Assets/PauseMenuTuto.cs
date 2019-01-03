using UnityEngine;

public class PauseMenuTuto : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "Scenes/MainMenu";
    public SceneFader sceneFader;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        ui.SetActive(!ui.activeSelf);

        Debug.Log("ACTIVE ? : " + ui.activeSelf); // affD

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

    public void Menu()
    {
        Toggle();
        MouseManager.lockMouse = false;                 // unlock the cursor
        sceneFader.FadeTo(menuSceneName);
    }
}
