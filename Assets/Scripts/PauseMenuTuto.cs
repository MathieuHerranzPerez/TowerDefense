using UnityEngine;

public class PauseMenuTuto : MonoBehaviour
{
    public GameObject ui;

    public GameObject tuto;
    public GameObject parameters;

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
            if (!tuto.activeSelf && !parameters.activeSelf)
                Toggle();
            else
            {
                HideTuto();
                HideParams();
            }
        }
    }

    public void Toggle()
    {
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

    public void Menu()
    {
        Toggle();
        MouseManager.lockMouse = false;                 // unlock the cursor
        sceneFader.FadeTo(menuSceneName);
    }

    public void Tuto()
    {
        if (tuto != null)
            tuto.SetActive(true);
    }

    public void HideTuto()
    {
        if (tuto != null)
            tuto.SetActive(false);
    }

    public void DisplayParams()
    {
        if (parameters != null)
            parameters.SetActive(true);
    }

    public void HideParams()
    {
        if (parameters != null)
            parameters.SetActive(false);
    }
}
