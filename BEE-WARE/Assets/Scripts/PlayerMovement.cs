using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;
    [SerializeField] float walkSpeed = 1f;
    [SerializeField] float runSpeed = 2f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float conveyerSpeed = 1f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);

    [SerializeField] AudioClip oof;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityScaleAtStart;
    float conveyerAddition = 0f;
    bool isAlive = true;
    bool isPaused = false;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if(!isAlive) {return;}
        ConveyerBelt();        
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnPause()
    {
        if(!isPaused)
        {
            FindObjectOfType<GameSession>().Pause();
            isAlive = false;
            isPaused = true;
            Time.timeScale = 0;
        }
        else if(isPaused)
        {
            FindObjectOfType<GameSession>().UnPause();
            isAlive = true;
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    void OnMove(InputValue value)
    {
        if(!isAlive) {return;}
        moveInput = value.Get<Vector2>(); 
    }
    
    void OnJump(InputValue value)
    {
        if(!isAlive) {return;}

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "RightConveyer", "LeftConveyer")))
        {
            return;
        }

        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    void OnRun()
    {
        playerSpeed = runSpeed;
    }

    void OnStopRun()
    {
        playerSpeed = walkSpeed;
    }
    
    void Run() 
        {
            
            Vector2 playerVelocity = new Vector2 ((moveInput.x * playerSpeed) + conveyerAddition, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;

            bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
            myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);

        }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        
    }

    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        { 
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
        
    }

    void ConveyerBelt()
    {
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("RightConveyer")))
        {
            conveyerAddition = conveyerSpeed;
        }
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("LeftConveyer")))
        {
            conveyerAddition = -conveyerSpeed;
        }
        else if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("RightConveyer", "LeftConveyer")))
        {
            conveyerAddition = 0;
        }

    }


    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            AudioSource.PlayClipAtPoint(oof, Camera.main.transform.position);
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
            Invoke("DoTheThing", 2f);
        }

    }

    void DoTheThing()
    {
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }


}
