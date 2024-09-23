using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour {

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip breakSound;

    void Start() {
        StartCoroutine("BreakingSound");
    }

    IEnumerator BreakingSound() {
        source.PlayOneShot(breakSound);
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
