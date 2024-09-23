using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRespawn : MonoBehaviour {
    
    [SerializeField] LayerMask p;
    [SerializeField] float checkRadius;
    [SerializeField] GameObject Box;
    bool cantRespawn;
    float respawnTimer = 4;

    void FixedUpdate() {
        cantRespawn = Physics2D.OverlapCircle(transform.position, checkRadius, p);
        respawnTimer -= Time.deltaTime;

        if(!cantRespawn && respawnTimer <= 0) {
            Instantiate(Box, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }

}
