using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbEnemies2 : StateMachineBehaviour {

    public GameObject Enemy;
    public Transform[] linePositions;
    OrbMain main;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        main = animator.GetComponent<OrbMain>();
        for (int i = 0; i < 2; i++)  {
            main.lines[i].enabled = true;
            main.lines[i].positionCount = 2;
            main.currentEnemies[i] = Instantiate(Enemy, main.enemyPositions[i + 2].position, main.enemyPositions[i + 2].transform.rotation);
            linePositions[i] = main.currentEnemies[i].transform;
            main.lines[i].SetPosition(0, main.handPositions[i].position);
            main.lines[i].SetPosition(1, linePositions[i].position);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        main.lines[0].SetPosition(0, main.handPositions[0].position);
        main.lines[0].SetPosition(1, linePositions[1].position);
        main.lines[1].SetPosition(0, main.handPositions[1].position);
        main.lines[1].SetPosition(1, linePositions[0].position);
    }
}
