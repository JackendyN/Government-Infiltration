using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour {

    [SerializeField] GameObject fragments;

    void OnDestroy() {
        if(this.gameObject.scene.isLoaded) {
            transform.DetachChildren();
            fragments.SetActive(true);
            fragments.transform.DetachChildren();
            Destroy(fragments);
        }
    }

}
