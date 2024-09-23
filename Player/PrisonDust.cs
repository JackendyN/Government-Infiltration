using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrisonDust : MonoBehaviour {

    [HideInInspector] public ParticleSystem particle;
    [SerializeField] int particleType;
    [HideInInspector] public static bool inPrison = true;
    Player player;

    void Start() {
        particle = GetComponent<ParticleSystem>();
        player = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update() {
        transform.localScale = new Vector3(player.scaler.x / 13, transform.localScale.y);
        inPrison = SceneManager.GetActiveScene().buildIndex <= 4;

        if (particleType == 1 && player.moveInput != 0 && inPrison && player.isGrounded) {
            particle.Play();
        } else {
            particle.Stop();
        }

        if(Input.GetKeyDown(KeyCode.Space) && inPrison && player.isGrounded && player.enabled) {
            JumpParticle();
        }
    }

    public void JumpParticle() {
        if(particleType == 2) {
            particle.Play();
        }
    }

    public void ExitPrison() {
        inPrison = false;
    }

}
