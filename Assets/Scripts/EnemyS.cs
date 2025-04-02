using UnityEngine;

public class EnemyS : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    public float health;
    public HealthBar healthBar;
    public float speed = 3;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDam(float damage)
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

    public void SlowDown()
    {
        animator.SetFloat("speed", 1);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
