using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour {

    public int HP;
    public int typeOfEnemy = 1;
    Guard guard;
    Rigidbody2D r;
    BoxCollider2D b;
    Animator a;
    AudioSource aS;
    public bool hasKey;
    public GameObject Key;
    
    // Start is called before the first frame update
    void Start() {
        r = GetComponent<Rigidbody2D>();
        a = GetComponent<Animator>();
        b = GetComponent<BoxCollider2D>();
        aS = GetComponent<AudioSource>();

        if(typeOfEnemy == 1){
            guard = GetComponent<Guard>();
        }
    }

    // Update is called once per frame
    void Update() {
        if(HP <= 0) {
            if(typeOfEnemy == 1) {
              guard.enabled = false;
            }
            r.velocity = new Vector2(0, r.velocity.y);
            r.isKinematic = true;
            b.enabled = false;
            aS.PlayOneShot(guard.guardFall);
            a.SetBool("Down", true);
            if(hasKey) {
                Instantiate(Key, new Vector2(transform.position.x, transform.position.y + 0.35f), Quaternion.identity);
            }
            this.enabled = false;
        }
    }

    public void TakeDamage(int Damage) {
        HP -= Damage;
        aS.PlayOneShot(guard.guardHit);
        a.SetTrigger("Hit");
    }
}
