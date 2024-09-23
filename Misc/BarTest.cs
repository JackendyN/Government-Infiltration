using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarTest : MonoBehaviour {

    // delete wen done
    public Transform pointTransform;
    Vector3 point;

    void Start()  {
        point = new Vector3(pointTransform.position.x, pointTransform.position.y, pointTransform.position.z);
    }

    // Update is called once per frame
    void Update() {
        transform.RotateAround(point, new Vector3(0f, 0f, 1f), 90f * Time.deltaTime);
    }
}
