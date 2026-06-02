using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public AiTarget ai;
    [SerializeField] Animator anim;

    // =====================================================
    // STATE DATA
    // =====================================================
    public enum State
    {
        Idle,
        Move,
        AttackWide,
        AttackBite,
        Dying
    }

    State prevState = State.Idle;
    public State currentState;

    // =====================================================
    // MAIN EXECUTION (like AiTarget.ExecuteAction)
    // =====================================================
    public void ExecuteAction()
    {
        if (currentState == State.Dying)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (stateInfo.IsName("Dying") && stateInfo.normalizedTime >= 1f)
            {
                gameObject.SetActive(false);
                Debug.Log("dying");
                return;
            }
        }
        currentState = CurrentState();

        if (prevState == currentState)
            return;

        prevState = currentState;

        switch (currentState)
        {
            case State.Move:
                anim.Play("Run", 0, 0f);
                break;

            case State.AttackWide:
                anim.Play("Scratch", 0, 0f);
                break;

            case State.AttackBite:
                anim.Play("NeckBite", 0, 0f);
                break;

            case State.Dying:
                anim.Play("Dying", 0, 0f);
                break;
        }
    }

    // =====================================================
    // STATE LOGIC (PURE, NO SIDE EFFECTS)
    // =====================================================
    State CurrentState()
    {
        // ABSOLUTE PRIORITY: DEATH
        if (ai.currentState == AiTarget.State.Dead)
            return State.Dying;

        // MOVEMENT
        if (ai.agent.speed > 0f)
            return State.Move;

        // ATTACK LOGIC
        switch (ai.currentState)
        {
            case AiTarget.State.MeetWall:
                return State.AttackWide;

            case AiTarget.State.MeetPlayer:
                return State.AttackBite;

            default:
                return State.Idle;
        }
    }
}