using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyWallRise : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float honeySpeed = 1f; 

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    
    public void HoneyStart()
    {
        myRigidBody.velocity = new Vector2 (0f, honeySpeed);
    }

}
