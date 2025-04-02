using UnityEngine;

public class Henchman : MonoBehaviour
{
    [Header("Henchman Properties")]
    [SerializeField] public float speed = 3f;
    [SerializeField] public int strength = 1;
    [SerializeField] public float maxLifetime = 5f; 
    [SerializeField] public string playerLayer = "Player"; 
    [SerializeField] public string wallLayer = "Wall";
    [SerializeField] public GameObject hitEffect;

    private Rigidbody2D rb;
    private float currentSpeed; // Store the signed speed
    private float currentLifetime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Henchman requires a Rigidbody2D component!");
            enabled = false; // Disable script if RB is missing
        }

        // Ensure the Rigidbody settings are appropriate
        rb.gravityScale = 1f; // Or your desired gravity
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // Prevent tilting
    }

    private void Start()
    {
        // Move towards player
         Vector2 direction = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
         currentSpeed = Mathf.Sign(direction.x) * speed; // Get initial direction (-1 or 1) * speed

        // Start moving right
        //currentSpeed = speed;

        // Set velocity
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y); 

        UpdateFacingDirection();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        currentLifetime += Time.fixedDeltaTime;
        if (currentLifetime >= maxLifetime)
        {
             Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;
        int targetPlayerLayer = LayerMask.NameToLayer(playerLayer);
        int targetWallLayer = LayerMask.NameToLayer(wallLayer);

        // Debug.Log($"Collided with: {collision.gameObject.name} (Layer: {LayerMask.LayerToName(collisionLayer)})");

        // Check if we hit the player
        if (collisionLayer == targetPlayerLayer)
        {
            Debug.Log("Hit player");
            HitPlayer(collision.gameObject);
            return;
        }

        if (collisionLayer == targetWallLayer)
        {
            // Check the collision normal to see if it's a vertical wall
            ContactPoint2D contact = collision.GetContact(0); // Get the first contact point

            // Check if the collision is (mostly) horizontal
            if (Mathf.Abs(contact.normal.x) > 0.1f && Mathf.Abs(contact.normal.x) > Mathf.Abs(contact.normal.y))
            {

                Debug.Log($"Hit Wall Horizontally. Normal: {contact.normal}");

                // Reverse the horizontal direction
                currentSpeed *= -1;

                // Apply the reversed velocity immediately to prevent sticking
                rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

                UpdateFacingDirection();
            }
            // else // Optional Debug: Hit ground/ceiling
            // {
            //      Debug.Log($"Hit Wall Vertically (Ground/Ceiling). Normal: {contact.normal}");
            // }
        }
    }

    void UpdateFacingDirection()
    {
        // Flip the sprite based on the direction of movement
        transform.localScale = new Vector3(Mathf.Sign(currentSpeed), 1, 1);

        // -- OR if you prefer rotating --
        // transform.right = new Vector2(currentSpeed, 0);
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