using UnityEngine;

public class Branch : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    int damage = 40;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        rb.angularVelocity = -540;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnemyS enemy = hitInfo.GetComponent<EnemyS>();
        BossHealth boss = hitInfo.GetComponent<BossHealth>();
        if (enemy != null)
        {
            enemy.TakeDam(damage);
        } else if (boss != null)
        {
            boss.TakeDamage(damage);
        }
        Destroy(gameObject);

    }
}
