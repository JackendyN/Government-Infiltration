using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignManager : MonoBehaviour {

    // Sign stuff is based off Brackeys
    [SerializeField] AudioSource audioSource;
    Queue<string> sentences;
    public GameObject SignUI;
    GameObject PauseUI;
    public Text SignText;
    Player player;

    void Start() {
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PauseUI = GameObject.FindGameObjectWithTag("Pause");
    }

    public void StartTablet(Sign dialogue){
        player.gameObject.GetComponent<AudioSource>().enabled = false;
        PauseUI.SetActive(false);
        SignUI.SetActive(true);
        sentences.Clear();
        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence(){
        audioSource.Play();
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        SignText.text = sentence;
    }

    void EndDialogue(){
        SignUI.SetActive(false);
        sentences.Clear();
        PauseUI.SetActive(true);
        player.gameObject.GetComponent<AudioSource>().enabled = true;
        player.enabled = true;
    }
}


