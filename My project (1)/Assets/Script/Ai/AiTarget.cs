using UnityEngine;
using UnityEngine.AI;

public class AiTarget : MonoBehaviour
{
    // =====================================================
    // REFERENCES
    // =====================================================
    [SerializeField] private Transform wallCenter;

    public Transform targetPoint;
    private NavMeshAgent agent;

    

    // =====================================================
    // STATE DATA
    // =====================================================
    private GameObject wall;

    public enum State
    {
        nul,
        MeetWall,
        MeetPlayer
    }

    State prevState = State.nul;
    State currentState;

    // =====================================================
    // LIFECYCLE
    // =====================================================
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable()
    {
        
    }

    // =====================================================
    // WALL DETECTION (CACHED)
    // =====================================================
    void UpdateWall()
    {
        if (wall != null) return;
        Vector3 dir = (wallCenter.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, dir);

        int wallLayerMask = LayerMask.GetMask("Wall");

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, wallLayerMask))
        {
            wall = hit.collider.gameObject;
        }
        else
        {
            wall = null;
        }
    }

    // =====================================================
    // MAIN EXECUTION
    // =====================================================
    public void ExecuteAction()
    {
        UpdateWall();
        currentState = CurrentState();
        if (prevState == currentState) return;
        prevState = currentState;

        switch (currentState)
        {
            case State.MeetWall:
                if (targetPoint != null)
                    agent.SetDestination(targetPoint.position);
                break;

            case State.MeetPlayer:
                agent.SetDestination(Player.Instance.transform.position);
                break;
        }
    }

    // =====================================================
    // STATE LOGIC (STABLE)
    // =====================================================
    State CurrentState()
    {
        if (wall != null && wall.activeInHierarchy)
            return State.MeetWall;


        return State.MeetPlayer;
    }
}