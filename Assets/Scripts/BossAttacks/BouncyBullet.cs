using UnityEngine;

public class BouncyBullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField] public float speed = 10f;
    [SerializeField] public int strength = 1;
    [SerializeField] public float maxLifetime = 5f;
    [SerializeField] public string playerLayer;
    [SerializeField] public GameObject hitEffect;
    
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private float currentLifetime;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {

        

        // Random direction
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            /*float randomAngle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));*/

            // Look at player
            Vector2 direction = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;

            rb.linearVelocity = direction * speed;
            transform.right = rb.linearVelocity;
            
        }
    }
    
    private void FixedUpdate()
    {
        // Store the last velocity for bounce calculations
        lastVelocity = rb.linearVelocity;

        // Lifetime check
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= maxLifetime)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name + " - " + collision.gameObject.layer + " VS Player: " + LayerMask.NameToLayer(playerLayer));

        // If it's another bullet, don't collide
        

        // Check if we hit the player
        if (collision.gameObject.layer == LayerMask.NameToLayer(playerLayer))
        {
            Debug.Log("hit");
            
            HitPlayer(collision.gameObject);
            
            Destroy(gameObject);
        }
        
        // Handle wall bounce

        
        // Calculate bounce
        if(collision.gameObject.layer == 7) // 7 is the layer for walls
        {
            Debug.Log("Bounce");
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.linearVelocity = direction.normalized * speed;
        }

    

    }
    
    private void HitPlayer(GameObject player)
    {
        Debug.Log("Hit player!");
        
        // Spawn hit effect if assigned
        if (hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
        player.GetComponent<PlayerHealth>().TakeDamage(strength);
        
        Destroy(gameObject);
    }
    
}
