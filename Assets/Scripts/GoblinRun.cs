using UnityEngine;

public class GoblinRun : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed;
    Boss boss;
    public float attackRange;
    float slowDownCoolDown = 0;
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
        speed = animator.GetFloat("speed");
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //Debug.Log(attackCooldown);

        if (Vector2.Distance(player.position, rb.position) <= attackRange && attackCooldown <= 0)
        {
            animator.SetTrigger("Attack");
            attackCooldown = defaultAttackCooldown;
        }
        else
        {
            attackCooldown -= Time.fixedDeltaTime;
        }


        //slow check
        if (speed < 3)
        {
            slowDownCoolDown += Time.fixedDeltaTime;
            if (slowDownCoolDown > 5)
            {
                animator.SetFloat("speed", 3);
                slowDownCoolDown = 0;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

}
