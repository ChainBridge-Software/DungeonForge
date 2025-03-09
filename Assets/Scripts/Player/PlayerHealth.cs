using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Rigidbody2D rb;
    public Transform start;
    public HealthBar healthBar;



    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
        healthBar.SetHealth(health);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
            healthBar.SetHealth(maxHealth);
        }
    }

    public void Die()
    {
        rb.transform.position = start.transform.position;
        health = maxHealth;
    }


}
