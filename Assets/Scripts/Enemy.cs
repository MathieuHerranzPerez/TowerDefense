using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range(1f, 50f)]
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float health = 100f;
    public int worth = 50;

    public GameObject deathEffect;


	// Use this for initialization
	void Start ()
    {
        speed = startSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void TakeDamage(float amount)
    {
        health -= amount;
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

        // destroy the enemy
        Destroy(gameObject);
    }
}
