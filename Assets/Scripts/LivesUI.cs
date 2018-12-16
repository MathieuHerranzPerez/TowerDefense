using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour {

    //public Text livesText;
    public Image healthBar;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        healthBar.fillAmount = (float) PlayerStats.Lives / (float) PlayerStats.StartLives;
        // livesText.text = PlayerStats.Lives.ToString() + " Lives";
	}
}
