using UnityEngine;

[System.Serializable]
public class WaveEnemyType {

    public GameObject enemyPrefab;
    public int count;
    public float rate;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public int GetNbEnemies()
    {
        return count * enemyPrefab.GetComponent<Enemy>().GetNbEnemies();
    }
}
