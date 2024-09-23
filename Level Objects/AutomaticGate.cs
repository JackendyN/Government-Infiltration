using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGate : MonoBehaviour {
    
    public BoxCollider2D bo;
    Rigidbody2D rig;
    public float toGo;
    AudioSource aud;
    [SerializeField] AudioClip Impact;

    void Start() {
        rig = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }

    public void Fall() {
        rig.isKinematic = false;
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if(hit.gameObject.tag == "BoxEnable") {
            bo.enabled = true;
            aud.PlayOneShot(Impact);
            this.enabled = false;
        }
    }

}
