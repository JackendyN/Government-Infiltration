using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHook : MonoBehaviour {

    public Transform hookPosition;
    public float hookRadius;
    public LayerMask boxLayer;
    GameObject hookedBox;
    Collider2D hookOverlap;
    [HideInInspector] public bool isHooked;
    Animator playerAnimator;
    Player playerScript;
    float doublePressTimer;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip boxTake;

    void Start() {
        playerAnimator = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
    }

    void Update() {
        hookOverlap = Physics2D.OverlapCircle(new Vector2(hookPosition.position.x, hookPosition.position.y), hookRadius, boxLayer);
        doublePressTimer -= Time.deltaTime;
        if(hookOverlap != null && Input.GetKeyDown(KeyCode.H) && playerScript.isGrounded && doublePressTimer <= 0 && isHooked == false) {
            isHooked = true;
            audioSource.PlayOneShot(boxTake, 0.7f);
            doublePressTimer = 0.2f;
        }

        if(isHooked) {
            hookedBox = hookOverlap.gameObject;
            hookedBox.transform.position = new Vector2(hookPosition.position.x, hookPosition.position.y);
            hookedBox.transform.parent = hookPosition;
            playerAnimator.SetBool("isHooked", true);

            if(Input.GetKeyDown(KeyCode.H) && doublePressTimer <= 0) {
                isHooked = false;
                hookedBox.transform.parent = null;
                FallingBox Box = hookedBox.GetComponent<FallingBox>();
                Box.Falling = true;
                hookedBox = null;
                playerAnimator.SetBool("isHooked", false);
                doublePressTimer = 0.2f;
            }
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hookPosition.position, hookRadius);
    }

}
