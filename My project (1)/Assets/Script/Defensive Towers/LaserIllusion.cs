using UnityEngine;

public class LaserIllusion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    AttackDefensiveTower defensiveTower;
    [SerializeField]Transform sphere;
    [SerializeField] Transform sphereHold;
    Transform enemy;
    [SerializeField] float speed;
    void Awake()
    {
        defensiveTower = GetComponent<AttackDefensiveTower>();
    }

    private void OnEnable()
    {
        defensiveTower.onDealWithEnemies += GoToEnemy;
        defensiveTower.GetReady += SphereToOriginalPosition;
    }

    private void Start()
    {
        defensiveTower.onDealWithEnemies += GoToEnemy; 
        defensiveTower.GetReady +=  SphereToOriginalPosition;
    }

    private void OnDisable()
    {
        defensiveTower.onDealWithEnemies -= GoToEnemy; 
        defensiveTower.GetReady -=  SphereToOriginalPosition;
    }

    

    // Update is called once per frame
    void GoToEnemy()
    {
        Vector3 dir = (sphere.position - defensiveTower.designatedTarget.transform.position).normalized;
        float distance = Vector3.Distance(sphere.position, defensiveTower.designatedTarget.transform.position);
        if (distance > 0.1f)
        {
            sphere.transform.position += dir * speed * Time.deltaTime;
        }
    }

    void SphereToOriginalPosition()
    {
        sphere.transform.position = sphereHold.transform.position;
    }
}
