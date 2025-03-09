using UnityEngine;

public class bulletS : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    int damage = 25;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        EnemyS enemy = hitInfo.GetComponent<EnemyS>();
        if (enemy != null)
        {
            enemy.TakeDam(damage);
        }
        Destroy(gameObject);

    }
}
