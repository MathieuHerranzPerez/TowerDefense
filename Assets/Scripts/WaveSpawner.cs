using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class WaveSpawner : MonoBehaviour {

    public static WaveSpawner Instance;

    public int EnemiesAlive = 0;
    public Wave[] waveArray;

    public Transform spawnPoint;
    public float timeBetweenWaves = 20f;
    public Text waveCountdownText;

    public GameManager gameManager;

    public GameObject enemyContainer;       // to spawn the enemies inside

    // UI
    [Header("UI")]
    public GameObject nextWaveUI;
    public GameObject nextWaveContainerInfo;
    private bool needUpdateUI = true;

    [Header("Light")]
    public GameObject normalLight;
    public GameObject darkLight;
    private bool isDark = false;
    private bool hasWaveABoss = false;

    private float countdown = 60f;
    public int waveIndex = 0;      // TODO make it PRIVATE
    

	// Use this for initialization
	void Start ()
    {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // if all enemies are dead (or have reached the end)
        if (EnemiesAlive <= 0 && enemyContainer.transform.childCount == 0)
        {
            // WIN
            if (waveIndex == waveArray.Length)
            {
                gameManager.WinLevel();
                this.enabled = false;       // disable the script
            }
            else if (countdown <= 0f)
            {
                needUpdateUI = true;
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
            else
            {
                if(needUpdateUI)
                {
                    UpdateUI();
                    needUpdateUI = false;
                }
                // if the player cancel the countdown
                if (Input.GetKeyDown(KeyCode.P))
                {
                    countdown = Mathf.Min(1f, countdown);
                }
                else
                {
                    countdown -= Time.deltaTime;
                    countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

                    // change the light each 5 rounds
                    if (hasWaveABoss)
                    {
                        darkLight.SetActive(true);
                        normalLight.SetActive(false);
                        isDark = true;
                    }
                    else if (isDark)
                    {
                        normalLight.SetActive(true);
                        darkLight.SetActive(false);
                        isDark = false;
                    }
                }
                waveCountdownText.text = string.Format("{0:00.0}", countdown);      // update the countdown UI text
            }
        }
	}

    IEnumerator SpawnWave()
    {
        ++PlayerStats.Rounds;
        int tmpNbEnemies = 0;
        for (int i = 0; i < waveArray[waveIndex].waveEnemyTypeArray.Length; ++i)
        {
            WaveEnemyType wave = waveArray[waveIndex].waveEnemyTypeArray[i];
            tmpNbEnemies += wave.GetNbEnemies();
        }
        EnemiesAlive = tmpNbEnemies;

        for (int i = 0; i < waveArray[waveIndex].waveEnemyTypeArray.Length; ++i)
        {
            WaveEnemyType wave = waveArray[waveIndex].waveEnemyTypeArray[i];

            for (int j = 0; j < wave.count; ++j)
            {
                SpawnEnemy(wave.enemyPrefab);
                yield return new WaitForSeconds(1f / wave.rate);      // wait to spawn another
            }
        }

        ++waveIndex;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyGO = (GameObject) Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        enemyGO.transform.SetParent(enemyContainer.transform, true);
    }

    private void UpdateUI()
    {
        // clear the current UI
        foreach (Transform child in nextWaveUI.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        hasWaveABoss = false;
        // check if there is a next wave
        if(waveIndex < waveArray.Length)
        {
            // foreach enemy in the next wave
            foreach(WaveEnemyType waveEnemyType in waveArray[waveIndex].waveEnemyTypeArray)
            {
                if (waveEnemyType.enemyPrefab.GetComponent<Enemy>().isBoss)
                    hasWaveABoss = true;

                // fill the UI
                GameObject containerInfos = (GameObject)Instantiate(nextWaveContainerInfo);

                // get the image and the text
                GameObject imageGO = containerInfos.transform.GetChild(0).gameObject;
                Image image = imageGO.GetComponent<Image>();
                GameObject textGO = containerInfos.transform.GetChild(1).gameObject;
                Text text = textGO.GetComponent<Text>();

                // set the enemy material to the image
                image.material = waveEnemyType.enemyPrefab.GetComponent<Enemy>().uiMaterial;
                
                // set the number of enemy in the text
                text.text = "x " + waveEnemyType.count;

                RectTransform rt = containerInfos.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, 24);
                containerInfos.transform.SetParent(nextWaveUI.transform, false);
            }
        }
    }
}
