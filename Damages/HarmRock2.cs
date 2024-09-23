using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmRock2 : MonoBehaviour {
    // Used for boss guard only, used to have more unique code
    PlayerHealth player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        Physics2D.IgnoreLayerCollision(15, 16);
        Physics2D.IgnoreLayerCollision(15, 6);
        Physics2D.IgnoreLayerCollision(15, 7);
    }

    void Update() {
        transform.Translate(Vector2.right * Time.deltaTime * 11 * (transform.localScale.x / Mathf.Abs(transform.localScale.x)));
    }

    void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.tag == "Player"){
          player.TakeDamage(1);
        } else if(hit.gameObject.tag == "Guard"){
            EnemyHP hp = hit.gameObject.GetComponent<EnemyHP>();
            hp.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
