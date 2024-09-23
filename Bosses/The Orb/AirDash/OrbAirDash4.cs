using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAirDash4 : StateMachineBehaviour
{

    public Rigidbody2D r;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
         r = animator.GetComponent<Rigidbody2D>();
         BoxCollider2D b = animator.GetComponent<BoxCollider2D>();
         b.size = new Vector2(0.05123475f, 0.1486111f);

        Transform playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>();
        OrbMain main = animator.GetComponent<OrbMain>();
        if(playerPosition.position.x < animator.transform.position.x) {
            animator.transform.eulerAngles = new Vector3 (0, 0, 0);
            main.shockPositions[0].eulerAngles = new Vector3(0, 180, 0);
            main.shockPositions[1].eulerAngles = new Vector3(0, 0, 0);
        } else {
            animator.transform.eulerAngles = new Vector3 (0, 180, 0);
            main.shockPositions[0].eulerAngles = new Vector3(0, 0, 0);
            main.shockPositions[1].eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       r.velocity = new Vector2(0, -18f);
    }

}
