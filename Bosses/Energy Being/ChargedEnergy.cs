using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedEnergy : MonoBehaviour {

    Vector3 scaleIncrease;
    public Vector3 maxScale;
    public bool released;
    public GameObject creator;
    PlayerHealth player;
    BoxCollider2D colli;
    
    void Start() {
        scaleIncrease = new Vector3(0.009f, 0.009f, transform.localScale.z);
        maxScale = new Vector3(3.5f, 3.5f, transform.localScale.z);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        colli = GetComponent<BoxCollider2D>();
        colli.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(released) {
            transform.Translate(transform.right * 5.5f * Time.deltaTime, Space.World);
            colli.enabled = true;
        } else {
            transform.localScale += scaleIncrease;
        }
    }

    void OnCollisionEnter2D(Collision2D hit){
        creator.GetComponent<EnergyBeing>().Reroll(4); 

        if(hit.gameObject.tag == "Player"){
          player.TakeDamage(2);
        } else if(hit.gameObject.name == "Machine Glass") {
            MachineGlass machineGlass = hit.gameObject.GetComponent<MachineGlass>();
            machineGlass.hitsTaken++;
            machineGlass.machineGlassAudio.Play();
        }

        Destroy(gameObject);
    }
}
