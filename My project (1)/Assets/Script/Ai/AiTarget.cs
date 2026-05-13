 // AiTarget.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
    
public class AiTarget : MonoBehaviour 
{
   //Coroutine seconds;SS
   public Transform wallCenter;
   GameObject wall;
   NavMeshAgent agent;
   bool wallactive;
   bool WallActive
   {
      get;
   }
   string tagWalls = "Wall";
   [SerializeField] Vector3 goal;
   private void Awake()
   {
      agent = GetComponent<NavMeshAgent>();
   }
   void Start() 
   {
      //StartCoroutine(LookForAgent());
 
      // GoTo();;
       
   }
   void OnEnable() 
   {
      //StartCoroutine(LookForAgent());

      // GoTo();;
       
   }



   [ContextMenu("go")]
   public void GoTo()  { goal = OnLookOutFromStart(); agent.SetDestination(goal);}

 [ContextMenu("CastRay")]
   Vector3 OnLookOutFromStart()
   {
      Vector3 dir = (wallCenter.position - transform.position).normalized;
      Ray ray = new Ray(transform.position, dir);
      RaycastHit hit;
      int wallLayerMask = LayerMask.GetMask("Wall");
      if (Physics.Raycast(ray, out hit, 1000f, wallLayerMask))
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

[ContextMenu("playeo")]
   public void GoMeetPlayer() => agent.SetDestination(Player.Instance.gameObject.transform.position);
   
     
   /* IEnumerator LookForAgent()
   {
      while(true)
      {
        if (wall)         
         yield return new WaitForSeconds(.33f);
      }
   } */
    
}