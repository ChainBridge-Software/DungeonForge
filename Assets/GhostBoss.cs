using UnityEngine;

public class GhostBoss : MonoBehaviour
{
    private Transform player;

    public bool isFlipped = false;

    public bool isTeleporting;
    public float teleportRange = 10;
    public float attackRange;
    public LayerMask attackMask;
    public int strength;
    public Rigidbody2D rb;
    public GameObject genyoPrefab;
    public Transform jobbGenyo, balGenyo;
    public void Awake()
    {
        player = GameObject.Find("Player").transform;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void LookAtPlayer_SpriteFlip()
    {
        // Do the same thing as above, but with the sprite renderer
        if (transform.position.x > player.position.x && isFlipped)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isFlipped = true;
        }
    }
    

    public void Teleport()
    {
        isTeleporting = true;
        float x = Random.Range(2, 15);
        float y;
        if (x > 10)
            y = Random.Range(4, 14);
        else
            y = Random.Range(10, 16);
        Vector2 randomOffset = new Vector2(x,y);
        Vector2 newPosition = (Vector2)player.position + randomOffset;
        transform.position = newPosition;
        isTeleporting = false;
    }
    public void TeleportV2()
    {
        isTeleporting = true;
        float x = Random.Range(5, 15);
        float y = Random.Range(0, 2);
        
        Vector2 randomOffset = new Vector2(x, y);
        Vector2 newPosition = (Vector2)player.position + randomOffset;
        transform.position = newPosition;
        isTeleporting = false;
    }

    public void SideSlash()
    {
        rb.linearVelocity = new Vector2(0,0);
        Vector3 pos = transform.position;
        pos += transform.right * (float)-1;
        pos += transform.up * (float)1;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, 6.2f, attackMask);
        if (colInfo != null)
        {
            Debug.Log("hit");
            colInfo.GetComponent<PlayerHealth>().TakeDamage(strength);

        }

    }

    public void DownSlash()
    {
        Vector3 pos = transform.position;

        pos += transform.up * (float)-2;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, 5.4f, attackMask);
        if (colInfo != null)
        {
            Debug.Log("hit");
            colInfo.GetComponent<PlayerHealth>().TakeDamage(Mathf.RoundToInt(strength*1.2f));

        }

    }

    public void Genyozas()
    {
        Instantiate(genyoPrefab, jobbGenyo.position, jobbGenyo.rotation);
        Instantiate(genyoPrefab, balGenyo.position, balGenyo.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        // pos += transform.right * (float)-1;
        pos += transform.up * (float)-2;
        if (pos == null)
            return;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
