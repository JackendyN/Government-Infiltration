using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour {

    public GameObject Guards;
    public BoxCollider2D glass;
    public Guard currentGuard;

    void Start() {
        glass.enabled = false;
        currentGuard.enabled = true;
        Rigidbody2D grb = currentGuard.GetComponent<Rigidbody2D>();
        grb.constraints = RigidbodyConstraints2D.None;
        grb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
