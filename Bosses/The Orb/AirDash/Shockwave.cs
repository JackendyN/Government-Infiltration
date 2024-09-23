using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour {

    void Start() {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject projectile in projectiles) {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), projectile.GetComponent<BoxCollider2D>());
        }
    }

    void Update() {
        transform.Translate(Vector2.left * Time.deltaTime * 15);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.name == "Adventurer") {
            PlayerHealth adv = collision.gameObject.GetComponent<PlayerHealth>();
            adv.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
