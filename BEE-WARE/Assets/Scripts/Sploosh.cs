using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sploosh : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CircleCollider2D myCircleCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCircleCollider = GetComponent<CircleCollider2D>();

        myRigidBody.gravityScale = 0;
        Invoke("Falling", 0.3f);
    }

    void Update()
    {
        if(myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Player")))
        {
            myAnimator.SetTrigger("Sploosh");
            Invoke("noMoreSploosh", 0.3f);
        }
    }

    void Falling()
    {
        myRigidBody.gravityScale = 1;
        myAnimator.SetTrigger("Fell");
    }
    void noMoreSploosh()
    {
        Destroy(gameObject);
    }

}
