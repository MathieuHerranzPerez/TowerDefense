using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waveArray;

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 6f;
    public Text waveCountdownText;

    public GameManager gameManager;

    private float countdown = 2f;
    private int waveIndex = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // if all enemies are dead (or have reached the end)
        if (EnemiesAlive <= 0)
        {
            // WIN
            if (waveIndex == waveArray.Length)
            {
                gameManager.WinLevel();
                this.enabled = false;       // disable the script
            }
            else if (countdown <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;
            }
            else
            {

                countdown -= Time.deltaTime;

                countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

                waveCountdownText.text = string.Format("{0:00.0}", countdown);      // update the countdown UI text
            }
        }
	}

    IEnumerator SpawnWave()
    {
        ++PlayerStats.Rounds;

        Wave wave = waveArray[waveIndex];

        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; ++i)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(1f / wave.rate);      // wait to spawn another
        }

        ++waveIndex;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
