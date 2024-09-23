using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int index;
    Player player;
    public Vector2 pos;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
    public void Enter(){
        player.LoadPlayerScene(pos, index);
    }
}
