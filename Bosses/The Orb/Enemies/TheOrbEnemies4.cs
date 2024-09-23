using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbEnemies4 : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        OrbMain main = animator.GetComponent<OrbMain>();
        main.KinematicSwitch(false);
        main.canCheck = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       animator.transform.Translate(Vector2.down * 0.08f * Time.deltaTime);
    }

}
