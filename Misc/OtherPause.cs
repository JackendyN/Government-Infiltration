using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPause : MonoBehaviour {

    [SerializeField] GameObject pause;

    public void EnablePause() {
        pause.SetActive(true);
    }
}
