using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;
    void Start()
    {
        myRigidBody= GetComponent<Rigidbody2D>();
        myBoxCollider= GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Move();   
    }
    void Move()
    {
        myRigidBody.velocity = new Vector2(speed , myRigidBody.velocity.y) ; 

    }
    void OnTriggerExit2D(Collider2D other)
    {
        speed = -speed;
        FlipEnemy();
        
    }

    void FlipEnemy()
    {
        transform.localScale= new Vector2 (-(MathF.Sign(myRigidBody.velocity.x)),1f) ;

    }
}
