using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbLightning : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    
       if(playerPosition.position.x < animator.transform.position.x) {
            animator.SetInteger("Side", 1);
       } else {
            animator.SetInteger("Side", 2);
       }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       animator.transform.position = Vector2.Lerp(animator.transform.position, new Vector2(4.76f, 0.55f), 2f * Time.deltaTime);
    }

}
