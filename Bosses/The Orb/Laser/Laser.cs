using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    BoxCollider2D Box;
    AudioSource sound;
    [SerializeField] AudioClip indicator;
    [SerializeField] AudioClip laser;

    void Start() {
        Box = GetComponent<BoxCollider2D>();
        sound = GetComponent<AudioSource>();
        Box.size = new Vector2(0, 0);
    }
    
    void TriggerUpdate(float Y) {
        Box.size = new Vector2(0.32f, Y);
    }

    void DeleteLaser() {
        Destroy(gameObject);
    }

    void PlayIndicator() {
        sound.PlayOneShot(indicator);
    }

    void PlayLaser() {
        sound.PlayOneShot(laser);
    }
}
