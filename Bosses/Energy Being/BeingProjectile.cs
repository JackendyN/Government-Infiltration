using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeingProjectile : MonoBehaviour {
    
    PlayerHealth player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(10 * Time.deltaTime * Vector2.right);
    }
    
    void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.CompareTag("Player")) {
          player.TakeDamage(1);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D enter) {
        if(enter.gameObject.name == "ProjectileDestroyer") {
            Destroy(gameObject);
        }
    }
}
