using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmRock3 : MonoBehaviour {

    PlayerHealth player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update() {
        transform.Translate(Vector2.down * Time.deltaTime * 10);
    }

    void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.tag == "Player"){
          player.TakeDamage(1);
        } 

        Destroy(gameObject);
    }
}
