using UnityEngine;

public class BossRun_Stationary : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed;
    Boss boss;
    public float attackRange;
    float attackCooldown;
    public float defaultAttackCooldown = 2.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //boss.LookAtPlayer_SpriteFlip();

        //Debug.Log(attackCooldown);
        
        if (Vector2.Distance(player.position, rb.position) <= attackRange && attackCooldown <= 0)
        {
            Debug.Log("Boss Attacking - Root");
            animator.SetTrigger("Attack_Root");
            attackCooldown = defaultAttackCooldown;
        }
        else if(Vector2.Distance(player.position, rb.position) <= (attackRange*2) && attackCooldown <= 0)
        {
            Debug.Log("Boss Attacking - Spawn");
            animator.SetTrigger("Attack_Spawn");
            attackCooldown = defaultAttackCooldown;
        }
        else
        {
            attackCooldown -= Time.fixedDeltaTime;
        }


        //slow check
        /*if (speed < 3)
        {
            slowDownCoolDown += Time.fixedDeltaTime;
            if (slowDownCoolDown > 5)
            {
                animator.SetFloat("speed", 3);
                slowDownCoolDown = 0;
            }
        }*/
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack_Root");
        animator.ResetTrigger("Attack_Spawn");
    }



}
