using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbLightning4 : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OrbMain main = animator.GetComponent<OrbMain>();
        for (int i = 0; i < 7; i++) {
            Instantiate(main.bolt, main.boltPositions[i].position, animator.transform.rotation);
        }
    }

}
