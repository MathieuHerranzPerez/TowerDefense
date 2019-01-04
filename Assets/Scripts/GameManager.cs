using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject winLevelUI;

    public Image winBackgroundFlashImage;
    public AnimationCurve curveOut;
    public AnimationCurve curveIn;

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
        winLevelUI.SetActive(true);
        StartCoroutine(FlashInput());
    }


    IEnumerator FlashInput()
    {
        // save the InputField.textComponent color
        Color defaultColor = winBackgroundFlashImage.color;


        float t = 0f;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            float alpha = curveOut.Evaluate(t);
            winBackgroundFlashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return 0;             // skip to the next frame
        }
        t = 1f;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float alpha = curveIn.Evaluate(t);
            winBackgroundFlashImage.color = new Color(1f, 1f, 1f, alpha);
            yield return 0;             // skip to the next frame
        }
        
        Destroy(winBackgroundFlashImage.gameObject); // magic door closes - remove object

        WinLevelFct();
    }

    public void WinLevelFct()
    {
        MouseManager.lockMouse = false;                 // unlock the cursor
        GameIsOver = true;
        Time.timeScale = 0f;
    }
}
