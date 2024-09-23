using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBoxLine : MonoBehaviour {

    public LineRenderer line;
    public Transform handPosition, boxPosition;
    
    void Start() {
        line.positionCount = 2;
    }

    void Update() {
        line.SetPosition(0, handPosition.position);
        line.SetPosition(1, boxPosition.position);
    }
}
