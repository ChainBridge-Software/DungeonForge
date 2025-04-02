using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    public Rigidbody2D rb;
    public Transform start;
    public HealthBar healthBar;
    public bool damagable = true;
    public int defense;
    public int heals = 3;



    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        healthBar.SetHealth(health);
        Debug.Log("remaining helas: " + heals);
    }

    public void HealV2()
    {
        if (heals > 0)
        {
            health += maxHealth * 0.3f;
            if (health > maxHealth)
                health = maxHealth;
            healthBar.SetHealth(health);
            heals--;
        }

    }

    public void TakeDamage(int damage)
    {
        defense = GameObject.Find("StatManager").GetComponent<PlayerStats>().def;
        
        if (damagable)
        {
            float takenDamage = (float)damage / (1 + ( ((float)defense / 10)));
            Debug.Log("damage: " + takenDamage);
            health -= takenDamage;
            healthBar.SetHealth(health);
            if (health <= 0)
            {
                Die();
                healthBar.SetHealth(maxHealth);
            }
        }
    }

    public void Die()
    {
        // Check if the player has a save
        if (DataPersistenceManager.instance != null)
        {
            // Load the game
            DataPersistenceManager.instance.LoadGame();
        }
        else
        {
            Debug.LogError("DataPersistenceManager instance is null!");

            Transform start = GameObject.Find("Start").transform;
            
            transform.position = start.position;
        }

        health = maxHealth;
    }


}
