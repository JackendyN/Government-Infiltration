using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmRock : MonoBehaviour
{
    PlayerHealth player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update() {
        transform.Translate(Vector2.right * Time.deltaTime * 10 * (transform.localScale.x / Mathf.Abs(transform.localScale.x)));
    }

    void OnCollisionEnter2D(Collision2D hit){
        if(hit.gameObject.tag == "Player"){
          player.TakeDamage(1);
        } 
        
        Destroy(gameObject);
    }
}
