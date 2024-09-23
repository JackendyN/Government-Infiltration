using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Gate : MonoBehaviour {

    public int keysNeeded;
    Animator ani;
    SpriteRenderer spriteRender;
    public Sprite[] doors;
    BoxCollider2D box;
    AudioSource aud;
    [SerializeField] AudioClip OpenAudio;
 
    void Start() {
        box = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.sprite = doors[keysNeeded - 1];
    }

    // Update is called once per frame
    public void Open() {
        ani.SetTrigger("Open");
        aud.PlayOneShot(OpenAudio);
        gameObject.tag = "Untagged";
    }

    public void RemoveCollider() {
        box.enabled = false;
    }
}
