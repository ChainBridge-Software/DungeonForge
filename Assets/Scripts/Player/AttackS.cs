using UnityEngine;

public class AttackS : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    int meleeDam = 40;
    float meleeCoolDown = 0;
    float shootCoolDown = 0;

    public InputManager inputManager;
    private bool isAttacking;

    // Update is called once per frame
    void Update()
    {

        isAttacking = inputManager.GetBoolean("Attack");
        Debug.Log("Attack: " + isAttacking);

        if (isAttacking && meleeCoolDown>1)
        {
            animator.SetTrigger("Attack");
            meleeCoolDown =0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && shootCoolDown>1.7)
        {
            Shoot();
            shootCoolDown = 0;
        }
        meleeCoolDown += Time.deltaTime;
        shootCoolDown += Time.deltaTime;
    }

    void Attack()
    {
        // attack anim
        
        //enemies in range
        
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // dam enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyS>().TakeDam(meleeDam);
        }

    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
