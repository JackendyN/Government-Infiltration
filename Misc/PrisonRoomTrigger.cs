using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonRoomTrigger : MonoBehaviour {

    public GameObject[] guards;
    public Transform[] guardPositions;

    void OnTriggerEnter2D(Collider2D i) {
        if(i.gameObject.tag == "GuardTrigger") {
           Instantiate(guards[0], guardPositions[0].position, Quaternion.identity);
           Instantiate(guards[1], guardPositions[1].position, Quaternion.identity);
           Instantiate(guards[2], guardPositions[2].position, Quaternion.identity);
           Instantiate(guards[3], guardPositions[3].position, Quaternion.identity);
           Destroy(i.gameObject);
        }
    }
}
