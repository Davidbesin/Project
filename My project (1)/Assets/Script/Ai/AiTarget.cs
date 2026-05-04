 // AiTarget.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
    
public class AiTarget : MonoBehaviour 
{
   //Coroutine seconds;SS
   public Transform wallCenter;
   GameObject wall;
   Wall wallcomp;
   NavMeshAgent agent;
   bool wallactive;
   bool WallActive
   {
      get;
   }
   private void Awake()
   {
      agent = GetComponent<NavMeshAgent>();
      
    
   }
   void Start() 
   {
      //StartCoroutine(LookForAgent());
      agent.destination = OnLookOutFromStart() ; 
      
      wallcomp = wall.GetComponent<Wall>();
   }

   Vector3 OnLookOutFromStart()
   {
      Vector3 dir = (wallCenter.position - transform.position).normalized;
      Ray ray = new Ray(transform.position, dir);
      RaycastHit hit;
      int wallLayerMask = LayerMask.GetMask("Wall");
      if (Physics.Raycast(ray, out hit, 100f, wallLayerMask))
      {
         wall = hit.collider.gameObject;
         Vector3 hitpoint = hit.point;
         return hitpoint;
      }
      else
      {
         return transform.position;
      }
   }
   /* IEnumerator LookForAgent()
   {
      while(true)
      {
        if (wall)         
         yield return new WaitForSeconds(.33f);
      }
   } */
    
}