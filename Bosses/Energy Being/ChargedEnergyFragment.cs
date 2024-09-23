using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedEnergyFragment : MonoBehaviour {

    [SerializeField] float speed;
    
    void FixedUpdate() {
        transform.Translate(Vector2.up * speed);
    }

    void DestroyFragment() {
        Destroy(gameObject);
    }

}
