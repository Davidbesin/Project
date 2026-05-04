using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    [SerializeField] AttackDefensiveTower tower;   // Reference to the tower script

    void Update()
    {
        if (tower != null && tower.designatedTarget != null)
        {
            Vector3 targetPos = tower.designatedTarget.transform.position;
            transform.LookAt(targetPos);
        }
    }
}
