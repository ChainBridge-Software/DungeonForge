using UnityEngine;
using UnityEngine.Rendering;

public class AttackS : MonoBehaviour
{
    public Transform firePoint, slimePoint;
    public GameObject bulletPrefab, slimePrefab, branchPrefab;
    public Animator animator;
    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    int strength;
    float meleeCoolDown = 0;
    float shootCoolDown = 0;

    // Find the InputManager in the scene
    private InputManager inputManager;
    private bool isAttacking;
    private bool ability1;
    private bool ability2;
    private bool ability3;
    private float abilityCoolDown = 0;

    void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        strength = GameObject.Find("StatManager").GetComponent<PlayerStats>().strength;
        isAttacking = inputManager.GetBoolean("Attack");

        ability1 = inputManager.GetBoolean("Ability1");
        ability2 = inputManager.GetBoolean("Ability2");
        ability3 = inputManager.GetBoolean("Ability3");

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
        if (ability1 && abilityCoolDown > 0.5)
        {
            Debug.Log("Ability1");
            animator.SetTrigger("Ab1");
            abilityCoolDown = 0;
        }
        if (ability2 && abilityCoolDown > 0.5)
        {
            Debug.Log("Ability2");
            animator.SetTrigger("Ab2");
            abilityCoolDown = 0;
        }
        if (ability3 && abilityCoolDown > 0.5)
        {
            Debug.Log("Ability3");
            animator.SetTrigger("Ab3");
            abilityCoolDown = 0;
        }


        meleeCoolDown += Time.deltaTime;
        shootCoolDown += Time.deltaTime;
        abilityCoolDown += Time.deltaTime;
    }

    void BareHand1()
    {
        Attack(0.8f, strength);
    }
    void BareHand2()
    {
        Attack(0.7f, strength * 1.2f);
    }
    void BareHandAir()
    {
        Attack(0.5f, strength);
    }
    void Sword1()
    {
        Attack(1.3f, strength);
    }
    void Sword2()
    {
        Attack(1f, strength*1.3f);
    }
    void SwordAir()
    {
        Attack(0.8f, strength*1.1f);
    }
    void Mace1()
    {
        Attack(1.13f, strength);
    }
    void Mace2()
    {
        Attack(1.04f, strength * 0.6f);
    }
    void MaceAir()
    {
        Attack(0.9f, strength * 1.1f);
    }
    void Axe1()
    {
        Attack(1.25f, strength);
    }
    void Axe2()
    {
        Attack(1.7f, strength * 1.5f);
    }
    void AxeAir()
    {
        Attack(0.9f, strength);
    }

    void Attack(float range, float damage)
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
                    enemy.GetComponent<BossHealth>().TakeDamage(damage);
                }
                else
                {
                    Debug.Log("We hit " + enemy.name + " - not enemy or boss? Wtf????");
                }

            }
            else
            {
                Debug.Log("We hit " + enemy.name + " - regular enemy");
                enemy.GetComponent<EnemyS>().TakeDam(damage);
                Debug.Log("dam: " + damage);
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

    void Branch()
    {
        Instantiate(branchPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("rotation: " + firePoint.rotation);
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
