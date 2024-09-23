using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGuardRock : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector2.left * Time.deltaTime * 13);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Adventurer") {
            PlayerHealth adv = collision.gameObject.GetComponent<PlayerHealth>();
            adv.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
