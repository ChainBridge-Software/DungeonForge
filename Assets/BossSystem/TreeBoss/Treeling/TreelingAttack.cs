using UnityEngine;

public class TreelingAttack : MonoBehaviour
{

    public int damage = 30;
    public float attackRange = 10f;
    public LayerMask attackMask;

    public void Update()
    {
        Collider2D colInfo = Physics2D.OverlapCircle(transform.position, attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
