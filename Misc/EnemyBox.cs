using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBox : MonoBehaviour {
   
    public Transform colliderr;
    public Transform colliderPosition;

    void Start() {
        Physics2D.IgnoreLayerCollision(6, 7);
    }
   
    void Update() {
        colliderr.position = colliderPosition.position;
    }
}
