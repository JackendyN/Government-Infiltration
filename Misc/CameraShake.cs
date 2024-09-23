using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraShake : MonoBehaviour {

    [SerializeField] CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin perlin;

    void Start() {
        perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetAmp(float amplitude) {
        perlin.m_AmplitudeGain = amplitude;
    }

    public void StartGame() { // Because it's on the gameobject anyway
        SceneManager.LoadScene(1);
    }

}
