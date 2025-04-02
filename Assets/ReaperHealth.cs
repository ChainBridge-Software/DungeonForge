using UnityEngine;

public class ReaperHealth : MonoBehaviour
{
    private float maxHealth;
    public float health;
    public Animator animator;
    public BossHealth bossHealth;
    // public HealthBar healthBar;
    // public Animator animator;

    void Start()
    {
        // healthBar.SetMaxHealth(Mathf.RoundToInt(health));
        
        
        animator = GetComponent<Animator>();
        bossHealth = GetComponent<BossHealth>();
        maxHealth = bossHealth.health;
        health = maxHealth;

    }
    private void Update()
    {
        if (!animator.GetBool("Ph2"))
        {
            health = bossHealth.health;
            if (health < (maxHealth / 1.3f))
            {
                Debug.Log("PH2!!!");
                animator.SetBool("Ph2", true);
            }
        }
    }
}
