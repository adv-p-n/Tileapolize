using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed=5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float defaultGravity = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    AudioSource deathSFX;
    bool isAlive = true;

    ParticleSystem deathParticle;
   void Start()
    {
        myRigidBody= GetComponent<Rigidbody2D>();
        myAnimator= GetComponent<Animator>();
        myBodyCollider= GetComponent<CapsuleCollider2D>();
        myFeetCollider= GetComponent<BoxCollider2D>();
        deathParticle= GetComponentInChildren<ParticleSystem>();
        deathSFX= GetComponent<AudioSource>();

    }

    void Update()
    {
        if (!isAlive){return;}
            Run(); 
            FlipSprite();
            Climb();
            Death();

    }

    void OnFire(InputValue value)
    {
        if (!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }


    void OnMove(InputValue value)
    {
        if (!isAlive){return;}
        moveInput= value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive){return;}
        int layerMask = LayerMask.GetMask("Ground");
        bool isTouchingLayer = myFeetCollider.IsTouchingLayers(layerMask);
        if (value.isPressed && isTouchingLayer)
        {
            myRigidBody.velocity = new Vector2 ( 0f , jumpSpeed);
        }
    }

     void Run()
     {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed , myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool hasHorizontalVelocity = MathF.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning" , hasHorizontalVelocity);                           
     }
    void FlipSprite()
    {
        bool hasHorizontalVelocity = MathF.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(hasHorizontalVelocity)
        {
            transform.localScale = new Vector2(MathF.Sign(myRigidBody.velocity.x) , 1f);
        }
    }

    void Climb()
    {

        bool isTouchingLadder = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"));
        if ( isTouchingLadder )
        {
            myRigidBody.gravityScale= 0f ;
            Vector2 playerVelocity = new Vector2 ( myRigidBody.velocity.x , moveInput.y*speed);
            myRigidBody.velocity = playerVelocity;

            bool hasVerticalVelocity = MathF.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbling" , hasVerticalVelocity);
        }
        else
        {
            myRigidBody.gravityScale= defaultGravity;
            myAnimator.SetBool("isClimbling" , false);
        }

    }
    
    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag=="Enemy")                     //This method is also correct
    //     {
    //         Death();
    //     }
    // }


    IEnumerator TDeath()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Spikes","Water")))
        {
            isAlive=false;
            myAnimator.SetTrigger("Died");
            deathParticle.Play();
            deathSFX.PlayOneShot(deathSFX.clip);
            yield return new WaitForSecondsRealtime(2);
            FindObjectOfType<GameSession>().playerDeath();
            Debug.Log("You Died !!!!!!!!!!");
        }
    }

        void Death()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Spikes","Water"))){return;}
        StartCoroutine(TDeath());
    }


}
