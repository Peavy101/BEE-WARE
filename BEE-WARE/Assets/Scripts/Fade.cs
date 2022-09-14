using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        myAnimator.SetBool("FadeOut", true);
    }
    public void FadeIn()
    {
        myAnimator.SetBool("FadeOut", false);
    }


}
