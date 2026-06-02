using UnityEngine;
using UnityEngine.AI;

public class AiTarget : MonoBehaviour
{
    // =====================================================
    // REFERENCES
    // =====================================================
    [SerializeField] private Transform wallCenter;

    public Transform targetPoint;
    public NavMeshAgent agent;
    public AnimationController anim;

    // =====================================================
    // STATE DATA
    // =====================================================
    private GameObject wall;

    private Wall walll;
    private BaseEnemyAI enemy;

    public float baseSpeed;

    public enum State
    {
        nul,
        MeetWall,
        MeetPlayer,
        Dead
    }

    State prevState = State.nul;
    public State currentState;

    // =====================================================
    // LIFECYCLE
    // =====================================================
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<AnimationController>();
        enemy = GetComponent<BaseEnemyAI>();
    }

    private void OnEnable()
    {
        wall = null;
        walll = null;

        prevState = State.nul;
        currentState = State.nul;

        agent.ResetPath();
        agent.isStopped = false;
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
            walll = wall.GetComponent<Wall>();
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

        if (prevState == currentState)
            return;

        prevState = currentState;

        switch (currentState)
        {
            case State.MeetWall:
                agent.isStopped = false;

                if (targetPoint != null)
                    agent.SetDestination(targetPoint.position);
                break;

            case State.MeetPlayer:
                agent.isStopped = false;
                agent.SetDestination(Player.Instance.transform.position);
                break;

            case State.Dead:
                agent.ResetPath();
                agent.isStopped = true;
                break;
        }
    }

    // =====================================================
    // STATE LOGIC
    // =====================================================
    State CurrentState()
    {
        if (enemy != null && enemy.Health <= 0)
            return State.Dead;

        if (walll != null && walll.Health > 0)
            return State.MeetWall;

        return State.MeetPlayer;
    }
}