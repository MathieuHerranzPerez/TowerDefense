using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyMovement))]
public class Enemy : MonoBehaviour {

    [Range(1f, 50f)]
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100f;
    private float health;
    public int worth = 50;

    public GameObject[] spawnWhenDieArray;

    public GameObject deathEffect;
    public GameObject hitEffect;

    [Header("Unity Stuff")]
    public Image healthBar;
    public GameObject healthBarCanvas;

    // Use this for initialization
    void Start ()
    {
        speed = startSpeed;
        health = startHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void TakeDamage(float amount)
    {
        healthBarCanvas.SetActive(true);

        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float amount, Vector3 pos, Vector3 normal)
    {
        HitEffect(pos, normal);
        TakeDamage(amount);
    }

    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);
    }

    private void Die()
    {
        // add money to the user
        PlayerStats.Money += worth;

        // effect on death
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 3f);

        if(spawnWhenDieArray != null)
        {
            foreach(GameObject go in spawnWhenDieArray)
            {
                GameObject gTmp = (GameObject) Instantiate(go, transform.position, Quaternion.identity);
                // give it the next waypoint
                EnemyMovement emChild = gTmp.GetComponent<EnemyMovement>();
                emChild.SetWaypoint(this.GetComponent<EnemyMovement>().GetWaypoint());
            }
        }

        //--WaveSpawner.EnemiesAlive;
       // Debug.Log("Die : " + WaveSpawner.EnemiesAlive); //affD

        // destroy the enemy
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Die : " + WaveSpawner.EnemiesAlive); //affD
        --WaveSpawner.EnemiesAlive;
    }

    private void HitEffect(Vector3 pos, Vector3 normal)
    {
        GameObject effect = Instantiate(hitEffect, pos, transform.rotation);
        Destroy(effect, 2f);
    }

    /**
     * Return the number of children + 1 (this)
     */ 
    public int GetNbEnemies()
    {
        int nbEnemies = 1;
        if (spawnWhenDieArray.Length != 0)
        {
            foreach(GameObject goChild in spawnWhenDieArray)
            {
                Enemy child = goChild.GetComponent<Enemy>();
                nbEnemies += child.GetNbEnemies();
            }
            Debug.Log("container : " + nbEnemies); // affD
        }
        return nbEnemies;
    }
}
