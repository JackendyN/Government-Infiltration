using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PipeMachine : MonoBehaviour {

    [SerializeField] Player player;
    [SerializeField] PlayableDirector timeline;
    [SerializeField] BoxCollider2D[] extraColliders;
    [SerializeField] SpriteRenderer interiorRenderer, exteriorRenderer;
    [SerializeField] ParticleSystem smokeIn, smokeOut;
    int currentSystem;

    public void StartMachine() {
        player.SelfDisable();
        timeline.Play();
    }

    public void EndMachine() {
        GetComponent<BoxCollider2D>().enabled = false;
        extraColliders[0].enabled = false;
        extraColliders[1].enabled = false;
        interiorRenderer.sortingOrder = 3;
        exteriorRenderer.sortingOrder = 4;
        player.enabled = true;
        timeline.Stop();
    }

    public void ToggleSmoke(int toggle) {
        if(currentSystem == 1) {
            if(toggle == 1) {
                smokeIn.Play();
            } else {
                smokeIn.Stop();
            }
        } else {
            if (toggle == 1) {
                smokeOut.Play();
            } else {
                smokeOut.Stop();
            }
        }
    }

    public void ChangeSystem(int change) {
        currentSystem = change;
    }

}
