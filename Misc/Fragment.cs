using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour {

    [SerializeField] string[] ignoredLayers;
    [SerializeField] float despawnTimer;

    void Start() {
        foreach (string layer in ignoredLayers) {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer(layer));
        }
    }

    void FixedUpdate() {
        despawnTimer -= Time.deltaTime;
        if(despawnTimer <= 0) {
            Destroy(gameObject);
        }
    }

}
