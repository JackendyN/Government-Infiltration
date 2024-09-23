using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MachineButton : MonoBehaviour {
    
    [SerializeField] BeingManager BM;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource machineSource;
    [SerializeField] Light2D light2D;
    [SerializeField] Light2D beingLight;

    public void Press() {
        BM.Death();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.enabled = false;
        light2D.intensity = 0;
        beingLight.intensity = 0;
        audioSource.Play();
        machineSource.Play();
    }
}
