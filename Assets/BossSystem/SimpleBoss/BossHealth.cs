using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 150;
    public HealthBar healthBar;
    public Animator animator;

    void Start()
    {
        healthBar.SetMaxHealth(Mathf.RoundToInt(health));
    }

    public void TakeDamage(float damage)
    {
        animator.SetTrigger("Damage");
        health -= damage;
        healthBar.SetHealth(Mathf.RoundToInt(health));
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
