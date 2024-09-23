using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Player player;
    bool moveRight;
    bool moveUp;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(player.LauncherUpgrade() && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
            moveUp = true;
        } else if(player.facingRight == true){
            moveRight = true;
        } else if(player.facingRight == false){
            moveRight = false;
        }
    }

    void Update() {
        if(moveUp) {
            transform.Translate(Vector2.up * Time.deltaTime * 10);
        } else if(moveRight == false){
            transform.Translate(Vector2.right * Time.deltaTime * 10);
        } else if(moveRight == true){
            transform.Translate(10 * Time.deltaTime * Vector2.left);
        } 
    }

    void OnCollisionEnter2D(Collision2D hit){
            Destroy(gameObject);
        if(hit.gameObject.tag == "Breakable"){
        Destroy(hit.gameObject);  
        } else if(hit.gameObject.tag == "Guard"){
            EnemyHP hp = hit.gameObject.GetComponent<EnemyHP>();
            hp.TakeDamage(1);
        } else if(hit.gameObject.tag == "BossGuardLauncher"){
            BossGuardLauncher BGL = hit.gameObject.GetComponent<BossGuardLauncher>();
            BGL.takeDamage();
        } else if(hit.gameObject.name == "Button") {
            OrbMachine OB = hit.gameObject.GetComponent<OrbMachine>();
            if(hit.gameObject.transform.parent.parent == null) {
                OB.machineOff = true;
            }
        } else if(hit.gameObject.name == "Machine Button") {
            MachineButton MB = hit.gameObject.GetComponent<MachineButton>();
            MB.Press();
        } else if(hit.gameObject.name == "The Orb") {
            OrbMain OM = hit.gameObject.GetComponent<OrbMain>();
            OM.currentHealth -= 1;
        }
    }
}
