using UnityEngine;
using UnityEngine.Rendering;

public class AttackS : MonoBehaviour
{
    public Transform firePoint, slimePoint;
    public GameObject bulletPrefab, slimePrefab;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange;
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
        /*
         * REGI, CSAK 1 utesnel mukszik, de megtartom ha az ujjal baj lenne
        if (isAttacking && meleeCoolDown >0.8)
        {
            animator.SetTrigger("Attack");
            meleeCoolDown = 0;
            Attack();
        }
        */

        if (isAttacking && animator.GetBool("Attacking") && meleeCoolDown > 0.2)
        {
            animator.SetBool("SecondAttack", true);
            animator.SetBool("Attacking", false);
        }
        else if (isAttacking && meleeCoolDown > 0.8)
        {
            animator.SetBool("SecondAttack", false);
            animator.SetTrigger("Attack");
            animator.SetBool("Attacking", true);
            meleeCoolDown = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && shootCoolDown>1.7)
        {
            Shoot();
            shootCoolDown = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetTrigger("Ab1");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            animator.SetTrigger("Ab2");
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            animator.SetTrigger("Ab3");


        meleeCoolDown += Time.deltaTime;
        shootCoolDown += Time.deltaTime;
    }

    void BareHand1()
    {
        Attack(0.8f);
    }
    void BareHand2()
    {
        Attack(0.7f);
    }
    void BareHandAir()
    {
        Attack(0.5f);
    }
    void Sword1()
    {
        Attack(1.3f);
    }
    void Sword2()
    {
        Attack(1f);
    }
    void SwordAir()
    {
        Attack(0.8f);
    }
    void Mace1()
    {
        Attack(1.13f);
    }
    void Mace2()
    {
        Attack(1.04f);
    }
    void MaceAir()
    {
        Attack(0.9f);
    }
    void Axe1()
    {
        Attack(1.25f);
    }
    void Axe2()
    {
        Attack(1.7f);
    }
    void AxeAir()
    {
        Attack(0.9f);
    }

    void Attack(float range)
    {
        // attack anim
        
        //enemies in range
        
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayers);
        // dam enemies
        foreach(Collider2D enemy in hitEnemies)
        {
            // Check if Enemy or Boss by component
            if(enemy.GetComponent<EnemyS>() == null)
            {
                if(enemy.GetComponent<BossHealth>() != null)
                {
                    Debug.Log("We hit " + enemy.name + " - boss");
                    enemy.GetComponent<BossHealth>().TakeDamage(meleeDam);
                }
                else
                {
                    Debug.Log("We hit " + enemy.name + " - not enemy or boss? Wtf????");
                }

            }
            else
            {
                Debug.Log("We hit " + enemy.name + " - regular enemy");
                enemy.GetComponent<EnemyS>().TakeDam(meleeDam);
            }
        }

    }

    void Wink()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, 5, enemyLayers);
        // dam enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We winked " + enemy.name);
            enemy.GetComponent<EnemyS>().SlowDown();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void Slime()
    {
        Debug.Log("slimeolunk");
        Instantiate(slimePrefab, slimePoint.position, slimePoint.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
