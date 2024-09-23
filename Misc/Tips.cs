using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tips : MonoBehaviour {

    public int Type = 1;
    public Image Tip;
    public char keyToPress;
    public float visibilitySpeed;
    float visibility;
    public Text[] keyText;
    public bool visible;

    void Awake() {
        DontDestroyOnLoad(this.gameObject.transform.parent);
    }

    void Update() {
        if(Type == 2 && !visible && visibility >= 0) {
            visibility -= visibilitySpeed;
        } else if (Type == 1 || Type == 2 && visible && visibility <= 1) {
            visibility += visibilitySpeed;
        }

        Tip.color = new Color(Tip.color.r, Tip.color.b, Tip.color.g, visibility);
        for (int i = 0; i < keyText.Length; i++) {
            keyText[i].color = new Color(keyText[i].color.r, keyText[i].color.b, keyText[i].color.g, visibility);
        }

        foreach (char keyPressed in Input.inputString) {
            if(char.ToUpper(keyPressed) == keyToPress) {
                if(Type == 1 || Type == 2 && visible) {
                    Destroy(this.gameObject.transform.parent.gameObject);
                }
            }
        }
    }

}
