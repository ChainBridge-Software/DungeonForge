using UnityEngine;

public class Genyo : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    int damage = 90;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        PlayerHealth pl = hitInfo.GetComponent<PlayerHealth>();
        if (pl != null)
        {
            pl.TakeDamage(damage);
        }
        Destroy(gameObject);

    }
}
