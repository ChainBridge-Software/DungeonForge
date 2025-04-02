using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    public int strength = 5;
    public float attackRange = 0.69f;
    public LayerMask attackMask;



    public void Attack()
    {

        Vector3 pos = transform.position;
        pos += transform.right * (float)-1;
        pos += transform.up * (float)1;
        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            Debug.Log("hit");
            colInfo.GetComponent<PlayerHealth>().TakeDamage(strength);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * (float)-1;
        pos += transform.up * (float)1;
        if (pos == null)
            return;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
