using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGuard : MonoBehaviour {
    
    public Transform rockPosition;
    public GameObject rock;
    [SerializeField] AudioSource audioSource;

    public void SpawnRock() {
        Instantiate(rock, rockPosition.position, transform.rotation);
        audioSource.Play();
    }
}
