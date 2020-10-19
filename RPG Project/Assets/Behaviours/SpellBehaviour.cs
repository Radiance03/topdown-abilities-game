using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : StateMachineBehaviour
{

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Color tmp = animator.GetComponent<SpriteRenderer>().color; //stores color
        tmp.a = 0f; //stores alpha
        animator.GetComponent<SpriteRenderer>().color = tmp; //update color

        Destroy(animator.gameObject,7);
    }   
}
