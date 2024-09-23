using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBox : MonoBehaviour {
    
    public bool Falling;
    Vector3 initialPosition;
    [SerializeField] GameObject respawn;
    [SerializeField] GameObject breakingSound;

    void Start() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Update() {
        if(Falling) {
            transform.Translate(Vector2.down * Time.deltaTime * 12);
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(Falling) {
            if (col.gameObject.tag == "BigGlass") {
                col.gameObject.transform.DetachChildren();
                Destroy(col.gameObject);
            }

            Instantiate(respawn, initialPosition, transform.rotation);
            Instantiate(breakingSound, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
