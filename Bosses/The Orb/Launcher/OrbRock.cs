using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbRock : MonoBehaviour {
    // Update is called once per frame
    void Update() {
        transform.Translate(Vector2.left * Time.deltaTime * 18);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Adventurer") {
            PlayerHealth adv = collision.gameObject.GetComponent<PlayerHealth>();
            adv.TakeDamage(2);
        }
        Destroy(gameObject);
    }
}
