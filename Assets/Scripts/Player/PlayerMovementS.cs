using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementS : MonoBehaviour
{

    public CharacterController2D controller;
    public float speed = 40;
    float horizontalMove = 0;
    bool jump = false;
    bool crouch = false;
    public Animator animator;

    public InputManager inputManager;



    private Vector2 plrMove;
    private bool isJumping;
    private bool isCrouching;



    // Update is called once per frame
    void Update()
    {


        plrMove = inputManager.GetVector2("Move");
        isJumping = inputManager.GetFloat("Jump") > 0;
        isCrouching = inputManager.GetFloat("Crouch") > 0;

        //Debug.Log(plrMove);
        /*Debug.Log(isJumping);
        Debug.Log(isCrouching);*/

        //horizontalMove = Input.GetAxisRaw("Horizontal") * speed;

        animator.SetFloat("Move", Mathf.Abs(plrMove.x));

        // Set Running animation speed to match player speed
        //animator.speed = Mathf.Abs(plrMove.x);

        if (isJumping)
        {
            Debug.Log("Jump");
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
        if (transform.position.y < -5)
        {
            GetComponent<PlayerHealth>().Die();
        }

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(plrMove.x, crouch, jump);
        jump = false;
    }


}
