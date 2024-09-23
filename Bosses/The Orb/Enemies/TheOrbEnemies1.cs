using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbEnemies1 : StateMachineBehaviour {

    public Transform Center;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       OrbMain main = animator.GetComponent<OrbMain>();
       main.maxEnemyRounds = 4;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       animator.transform.position = Vector2.Lerp(animator.transform.position, new Vector2(4.76f, 0.55f), 2f * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }

}
