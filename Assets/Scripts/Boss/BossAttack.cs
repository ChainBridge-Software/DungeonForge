using System.Xml.Serialization;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int strength = 15;
    public float attackRange = 2f;
    public LayerMask attackMask;

    

    public void Attack()
    {
        
        Vector3 pos = transform.position;
        pos += transform.right * (float)-0.7;
        pos += transform.up * (float)1.6;
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
        pos += transform.right * (float)-0.7;
        pos += transform.up * (float)1.3;
        if (pos == null)
            return;

        Gizmos.DrawWireSphere(pos, attackRange);
    }


}
