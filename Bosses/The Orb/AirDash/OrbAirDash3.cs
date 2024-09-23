using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAirDash3 : StateMachineBehaviour
{
    public Rigidbody2D r;
    Animator ani;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        r = animator.GetComponent<Rigidbody2D>();
        ani = animator;
        BoxCollider2D b = animator.GetComponent<BoxCollider2D>();
        b.size = new Vector2(0.06093919f, 0.03122854f);

        OrbMain main = animator.GetComponent<OrbMain>();
        if(main.lastStop.name == "DashStop1" || main.lastStop == null) {
            animator.transform.eulerAngles = new Vector3 (0, 0, 0);
        } else {
            animator.transform.eulerAngles = new Vector3 (0, 180, 0);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.transform.Translate(Vector2.left * Time.deltaTime * 13f);
        r.velocity = new Vector2(r.velocity.x, 0);
    }
    
    
}
