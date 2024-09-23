using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BeingManager : MonoBehaviour {
    
    public Transform[] beingPositions;
    public Transform machinePosition;
    public Transform currentPos;
    public LineRenderer line;
    public PlayableDirector startTimeline;
    ChangeCameraPosition cameraChangeScript;
    public EnergyBeing boss;
    public GameObject gate;
    BoxCollider2D bc2d;
    public bool goingDown;
    [SerializeField] SpriteRenderer buttonSprite, machineSprite;
    [SerializeField] Sprite buttonChange, machineChange;
    [SerializeField] AudioSource machineAudio;

    void Start() {
        line.enabled = false;
        cameraChangeScript = GetComponent<ChangeCameraPosition>();
        bc2d = GetComponent<BoxCollider2D>();
        line.positionCount = 2;
        currentPos = beingPositions[0];
    }

    // Update is called once per frame
    void Update() {
        line.SetPosition(0, machinePosition.position);
        line.SetPosition(1, currentPos.position);

        if(goingDown) {
            machinePosition.parent.Translate(Vector2.down * Time.deltaTime * 3);
        }

    }

    public void StartBossOpening() {
        gate.SetActive(true);
        cameraChangeScript.Toggle();
        bc2d.enabled = false;
        boss.gameObject.SetActive(true);
    }

    public void Death() {
        boss.animator.SetTrigger("Death");
        BoxCollider2D MachineCollider = machinePosition.parent.gameObject.GetComponent<BoxCollider2D>();
        MachineCollider.enabled = false;
        machineAudio.Stop();
        line.enabled = false;
        boss.enabled = false;
        machineSprite.sprite = machineChange;
        buttonSprite.sprite = buttonChange;
        goingDown = true;
    }

}

