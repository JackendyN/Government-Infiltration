using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {

    public GameObject bolt;
    AudioSource audioSource;
    [SerializeField] AudioClip indicator;
    [SerializeField] AudioClip lightning;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(indicator);
    }

    void SummonBolt() {
        bolt.SetActive(true);
        audioSource.PlayOneShot(lightning);
    }

    void RemoveBolt() {
        Destroy(gameObject);
    }
}
