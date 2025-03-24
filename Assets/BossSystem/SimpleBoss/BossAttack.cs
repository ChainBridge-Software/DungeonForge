using System.Xml.Serialization;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int strength = 15;
    public float attackRange = 2f;
    public LayerMask attackMask;

    public GameObject bubblePrefab;

    

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

    public void BlowBubble()
    {
        Vector3 pos = transform.position;
        pos += transform.right * 0.7f;
        pos += transform.up * (float)1.6;
        GameObject bubble = Instantiate(bubblePrefab, pos, transform.rotation);
        bubble.GetComponent<BouncyBullet>().maxLifetime = 5f;
        bubble.GetComponent<BouncyBullet>().speed = 5f;
        bubble.GetComponent<BouncyBullet>().strength = 10;
        bubble.GetComponent<BouncyBullet>().playerLayer = "Player";
        bubble.GetComponent<Rigidbody2D>().linearVelocity = transform.right * bubble.GetComponent<BouncyBullet>().speed;
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
