using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class OrbMachine : MonoBehaviour {

    GameObject machine;
    LineRenderer line;
    public Transform[] positions;
    public Transform originalPosition;
    public bool machineOff;

    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip power;
    [SerializeField] AudioClip powerOff;

    [SerializeField] Light2D light2D;

    // Start is called before the first frame update
    void Start() {
        machine = transform.parent.gameObject;
        line = machine.GetComponent<LineRenderer>();
        line.positionCount = positions.Length + 1;
    }

    // Update is called once per frame
    void Update() {

        if(!machineOff) {
            sound.PlayOneShot(power, 0.3f);
            line.SetPosition(0, originalPosition.position);
            for (int i = 0; i < positions.Length; i++) {
                line.SetPosition(1 + i, positions[i].position);
            }

        } else {
            line.enabled = false;
            sound.Stop();
            sound.loop = false;
            light2D.intensity = 0;
            Animator machineAnimator = machine.GetComponent<Animator>();
            machineAnimator.SetTrigger("MachineDisable");

            for (int i = 0; i < positions.Length; i++) {
                if(positions[i].gameObject != null) {
                    Destroy(positions[i].gameObject);
                }
            }

            Destroy(originalPosition.gameObject);
            sound.PlayOneShot(powerOff, 0.8f);
            this.enabled = false;
        }
    }
}
