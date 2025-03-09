using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int health;
    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDam(int damage)
    {
        animator.SetTrigger("Start");
        health -= damage;
        healthBar.SetHealth(health);
        Debug.Log(health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
