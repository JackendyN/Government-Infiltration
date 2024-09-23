using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {
    
    Player pl;
    Rigidbody2D rb2d;
    bool Lifting;
    Animator ani;
    public BoxCollider2D boxToDisable;
    AudioSource aud;
    [SerializeField] AudioClip Elevator;
    [SerializeField] AudioClip elevatorEnd;

    void Start() {
        pl = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        aud = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(Lifting) {
            pl.enabled = false;
            pl.animator.SetBool("isRunning", false);
            pl.animator.SetBool("isJumping", false);
            pl.rb.velocity = new Vector2(0, 0);
            pl.moveInput = 0;
            rb2d.velocity = new Vector2(rb2d.velocity.x, 12f);
        } 
    }

    public void StartLift() {
       Lifting = true;
       aud.PlayOneShot(Elevator);
       ani.SetTrigger("Closing");
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "StopLift") {
            Lifting = false;
            rb2d.velocity = new Vector2(0, 0);
            pl.enabled = true;
            rb2d.gravityScale = 0;
            rb2d.isKinematic = true;
            ani.SetTrigger("Opening");
            aud.Stop();
            aud.PlayOneShot(elevatorEnd);
            boxToDisable.enabled = false;
            pl.rb.velocity = new Vector2(0, 0);
            this.enabled = false;
        }
    }
}
