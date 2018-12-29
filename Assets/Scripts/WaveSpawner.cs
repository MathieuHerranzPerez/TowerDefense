using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public static int EnemiesAlive = 0;

    public Wave[] waveArray;

    public Transform spawnPoint;
    public float timeBetweenWaves = 10f;
    public Text waveCountdownText;

    public GameManager gameManager;

    public GameObject enemyContainer;       // to spawn the enemies inside

    private float countdown = 5f;
    private int waveIndex = 0;

	// Use this for initialization
	void Start ()
    {

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
}
