using UnityEngine;

public class GhostMove : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed;
    GhostBoss boss;
    public float attackRange;
    float slowDownCoolDown = 0;
    float attackCooldown;
    public float defaultAttackCooldown = 2.5f;
    public float magassag;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<GhostBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        speed = animator.GetFloat("speed");
        boss.LookAtPlayer();


        Vector2 target = new Vector2(player.position.x, player.position.y +3);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        //Debug.Log(attackCooldown);

        magassag = rb.position.y -target.y;
        animator.SetFloat("Magassag", magassag);


        if (Vector2.Distance(player.position, rb.position) <= attackRange && attackCooldown <= 0)
        {
            if (animator.GetBool("Ph2"))
            {
                animator.SetInteger("Rand", Random.Range(1,3));
                animator.SetInteger("Leszall", Random.Range(1, 4));
            }

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

