 // AiTarget.cs
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
    
public class AiTarget : MonoBehaviour 
{
   //Coroutine seconds;SS
   public Transform wallCenter;
   GameObject wall;
   public NavMeshAgent agent;
   
   bool wallactive;
   bool onTheWay;
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

   public enum State
   {
     GrandJourney,
     ChooseWall,
      GoMeetPlayer
   }

   [SerializeField]State currentState;


   [ContextMenu("go")]
   public void GoTo()
   {
      if (onTheWay) return;
      goal = OnLookOutFromStart();
      agent.SetDestination(goal);
      onTheWay = true;
   }

private void Update()
{
    currentState = ChooseState();
    Debug.Log(agent.destination);
}
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
         return wallCenter.position;
      } 
   }

   [ContextMenu("playeo")]
   
   public void ExecuteAction()
   {
      switch (currentState)
      {
         case State.GrandJourney:
         GoTo();
         break;

         case State.ChooseWall:
         agent.SetDestination(ChooseRandomPointWall(wall));
         break;

         case State.GoMeetPlayer:
         agent.SetDestination(Player.Instance.transform.position);
         break;

        // default:
      } 
   }
     
   State ChooseState()
   {
      if (wall != null ) return State.GrandJourney;
      float distance = Vector3.Distance(transform.position, agent.destination);
      if (!wall.activeInHierarchy) return State.GoMeetPlayer; 
      else
      {
         if (distance > 0.5f) return State.ChooseWall;
         else return State.GrandJourney;
      }
   }

   Vector3 ChooseRandomPointWall(GameObject col)
   {
      Collider collider = col.GetComponent<Collider>();
      Bounds bounds = collider.bounds;

      Vector3 point = new Vector3(
         Random.Range(bounds.min.x, bounds.max.x),
         col.transform.position.y,
         Random.Range(bounds.min.z, bounds.max.z)
      );

      return point;
   }

   
}