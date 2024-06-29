using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimator : MonoBehaviour
{
    public Animator animator;

    public void Run(){
        if(IsInvoking(nameof(Run)))return;

        animator.SetBool("jump", false);
        animator.SetBool("damage", false);

        animator.SetBool("run", true);
    }

    public void Jump(){
        //animator.SetBool("damage", false);
        //animator.SetBool("run", false);

        //animator.SetBool("jump", true);

        //Invoke(nameof(Run), 0.1f);
    }

    public void Damage(){
        animator.SetBool("jump", false);
        animator.SetBool("run", false);

        animator.SetBool("damage", true);

        Invoke(nameof(Run), 0.5f);
    }

    public void Death(){
        animator.SetBool("jump", false);
        animator.SetBool("damage", false);
        animator.SetBool("run", false);
        
        animator.SetBool("death", true);
    }
}
