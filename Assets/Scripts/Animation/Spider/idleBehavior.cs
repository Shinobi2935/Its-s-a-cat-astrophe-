using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = Random.Range(minTime, maxTime);
        animator.SetBool("jumped", false);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("jump");
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
