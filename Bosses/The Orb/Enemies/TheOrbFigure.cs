using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheOrbFigure : StateMachineBehaviour {

    AudioSource audioSource;
    [SerializeField] AudioClip dash;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        audioSource = animator.GetComponent<AudioSource>();
        audioSource.PlayOneShot(dash);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       animator.transform.Translate(Vector2.left * Time.deltaTime * 16);
    }

}
