using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tablet : MonoBehaviour {
    
     public Sign text;

     public void TriggerTablet() {
         FindObjectOfType<SignManager>().StartTablet(text);
     }

}
