using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAirDash2 : StateMachineBehaviour {

    public Rigidbody2D r;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        r = animator.GetComponent<Rigidbody2D>();
        OrbMain main = animator.GetComponent<OrbMain>();
        main.KinematicSwitch(false);
        r.velocity = new Vector2(r.velocity.x, 20f);
    }

}
