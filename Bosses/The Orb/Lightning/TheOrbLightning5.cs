using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbLightning5 : StateMachineBehaviour {

    OrbMain main;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        main = animator.GetComponent<OrbMain>();
        main.KinematicSwitch(false);
        main.canCheck = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       animator.transform.Translate(0.08f * Time.deltaTime * Vector2.down);
    }
    
}
