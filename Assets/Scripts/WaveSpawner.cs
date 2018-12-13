using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5.5f;
    public Text waveCountdownText;

    private float countdown = 2f;
    private int waveNumber = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(countdown).ToString();      // update the countdown UI text
	}

    IEnumerator SpawnWave()
    {
        ++waveNumber;
        for (int i = 0; i < waveNumber; ++i)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);      // wait 0.5s to spawn another
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
