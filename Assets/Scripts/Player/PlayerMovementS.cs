using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementS : MonoBehaviour
{

    public CharacterController2D controller;
    public float agility;
    bool jump = false;
    bool crouch = false;
    public Animator animator;
    public Rigidbody2D rb;

    private InputManager inputManager;

    // ++
    public Transform closeGround;
    [SerializeField] private LayerMask m_WhatIsGround;
    const float k_GroundedRadius = .2f;


    private Vector2 plrMove;
    private bool isJumping;
    private bool isCrouching;

    //dashhh
    private bool canDash = true;
    private bool isDashing = false;
    public float dashPower = 100f;
    private float dashCoolDown = 1f;
    private float dashTime = 0.2f;

    void Awake()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        plrMove = inputManager.GetVector2("Move");
        isJumping = inputManager.GetFloat("Jump") > 0;
        isCrouching = inputManager.GetFloat("Crouch") > 0;


        // Test input manager
        Debug.Log(inputManager.GetVector2("Move"));
        Debug.Log(inputManager.GetFloat("Jump"));
        Debug.Log(inputManager.GetFloat("Crouch"));

    }


    private float jumpCoolDown = 0.33f;
    private float groundTime = 0;
    public bool levegoben = false;
    public float meddigLevegoben = 0;

    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip footstepSound;

    // Update is called once per frame
    void Update()
    {

        // Debug.Log(inputManager.GetVector2("Move") + " " + inputManager.GetFloat("Jump") + " " + inputManager.GetFloat("Crouch"));
        PlayerStats ps = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        dashPower = ps.dashHossz;
        agility = ps.agility;
        if (isDashing)
        {
            return;
        }
        //dashh
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
        PlayerHealth ph = GameObject.Find("Player").GetComponent<PlayerHealth>();
        // healing

        if (Input.GetKeyDown(KeyCode.F) && ph.heals > 0 && ph.health < ph.maxHealth)
        {
            animator.SetTrigger("Heal");
        }

        plrMove = inputManager.GetVector2("Move");
        isJumping = inputManager.GetFloat("Jump") > 0;
        isCrouching = inputManager.GetFloat("Crouch") > 0;

        // Play step sound if player is moving
        if (plrMove.x != 0 && controller.m_Grounded)
        {
            if (!GetComponent<AudioSource>().isPlaying)
            {

                // Set step sound
                GetComponent<AudioSource>().clip = footstepSound;

                // Set speed of the step sound to match player speed
                GetComponent<AudioSource>().pitch = 1.5f*(Mathf.Abs(plrMove.x) / 2 + 0.5f);

                GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        //Debug.Log(plrMove);
        /*Debug.Log(isJumping);
        Debug.Log(isCrouching);*/

        //horizontalMove = Input.GetAxisRaw("Horizontal") * agility;

        animator.SetFloat("Move", Mathf.Abs(plrMove.x));
        // Set Running animation agility to match player agility
        //animator.agility = Mathf.Abs(plrMove.x);
        if (!levegoben)
        {
            groundTime += Time.deltaTime;

        }

        if (levegoben)
        {
            meddigLevegoben += Time.deltaTime;
        }

        // Play jump sound
        if(isJumping && levegoben == false)
            PlaySound(jumpSound);


        if (isJumping && groundTime > jumpCoolDown)
        {

            animator.ResetTrigger("Leesik");
            levegoben = true;

            jump = true;
            animator.SetBool("isJumping", true);
        }
        if (isCrouching)
        {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        //check if he's fallen 
        if (transform.position.y <= -5)
        {
            GetComponent<PlayerHealth>().Die();
        }
    }

    public void OnLanding()
    {
     if (controller.m_Grounded && meddigLevegoben > 0.3)
        {

            meddigLevegoben = 0;
            Debug.Log("leesunk");
            animator.SetBool("isJumping", false);
            animator.SetTrigger("Leesik");
            groundTime = 0;
            levegoben = false;

            // Play land sound
            PlaySound(landSound);
        }

    }

    void FixedUpdate()
    {
        if (isDashing)
            return;
        controller.Move(plrMove.x, crouch, jump && groundTime > 0.2);
        jump = false;
    }

    
    private IEnumerator Dash()
    {
        if (GameObject.Find("StatManager").GetComponent<PlayerStats>().invincibleDash)
        {
            Debug.Log("inv dash");
            animator.SetBool("invDash", true);
            GameObject.Find("Player").GetComponent<PlayerHealth>().damagable = false;
        }
            
        canDash = false;
        isDashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        animator.SetTrigger("Dash");
        rb.linearVelocity = new Vector2(transform.localScale.x * dashPower * (controller.m_FacingRight ? 1 : -1), 0);
        yield return new WaitForSeconds(dashTime);
        GameObject.Find("Player").GetComponent<PlayerHealth>().damagable = true;
        rb.gravityScale = gravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
        animator.SetBool("invDash", false);
    }


    void PlaySound(AudioClip sound)
    {
        // Set step sound
        GetComponent<AudioSource>().clip = sound;

        // Play once
        GetComponent<AudioSource>().PlayOneShot(sound);
    }
     

}
