using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 150;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
