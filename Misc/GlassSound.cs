using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSound : MonoBehaviour {

    [SerializeField] AudioSource audioSource;

    void OnDestroy() {
        if(this.gameObject.scene.isLoaded) {
            audioSource.enabled = true;
            audioSource.Play();
        }
    }

}
