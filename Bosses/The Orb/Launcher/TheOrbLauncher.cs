using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbLauncher : StateMachineBehaviour{

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
    
       if(playerPosition.position.x < animator.transform.position.x) {
            animator.transform.eulerAngles = new Vector3 (0, 0, 0);
       } else {
            animator.transform.eulerAngles = new Vector3 (0, 180, 0);
       }
    }

}
