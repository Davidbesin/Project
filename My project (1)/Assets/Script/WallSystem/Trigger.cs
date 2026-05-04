using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
void Start() 
   {
      StartCoroutine(Activate());
   }

   public bool own{get;private set;}
   public void SetBool(bool pool) {own = pool;}
   
   IEnumerator Activate()
   {
      while(true)
      {
         transform.gameObject.SetActive(true);
         yield return new WaitForSeconds(.20f);
      }
   }
}
