using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipSpawn : MonoBehaviour {
   
   public GameObject Tip;
   public Tips tipS;

   public void DisplayTip() {
      Tip.SetActive(true);
      Destroy(gameObject);
   }

   public void ToggleVisibility(bool V) {
      tipS.visible = V;
   }

}
