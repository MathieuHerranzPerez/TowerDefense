using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    [Range(1f, 50f)]
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100f;
    private float health;
    public int worth = 50;

    public GameObject deathEffect;

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

        --WaveSpawner.EnemiesAlive;

        // destroy the enemy
        Destroy(gameObject);
    }
}
