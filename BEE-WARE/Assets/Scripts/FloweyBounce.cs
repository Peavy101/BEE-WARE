using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloweyBounce : MonoBehaviour
{
    [SerializeField] AudioClip boing;

    Animator myAnimator;
    CapsuleCollider2D myFlowerCollider;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myFlowerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        FloweyBouncey();
    }

    void FloweyBouncey()
    {
        if(myFlowerCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            AudioSource.PlayClipAtPoint(boing, Camera.main.transform.position);
            myAnimator.SetBool("IsBounce", true);
        }
        else
        {
            myAnimator.SetBool("IsBounce", false);
        }


    }
}
