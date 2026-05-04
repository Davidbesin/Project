using UnityEngine;

public class Wall : MonoBehaviour, IHealth
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MeshRenderer mesh;
    BoxCollider collider;
    
    
    public bool PlayerSide => true;
    bool wallActive = true;
    public bool WallActive
    {
        get => wallActive;
        set 
        {
            wallActive = value;
            if (value)
            {
                 mesh.enabled = true;
               collider.enabled = true;
            }
            else
            {
               mesh.enabled = false;
               collider.enabled = false;

            }
        }
    }

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }
    
    [SerializeField] int health;
    public int Health
    {
        get => health;
        set => health = value;
    }


    public void TakeDamage(int damage)
    {
        Debug.Log("Ims supposed to be dying slowly");
        Health -= damage;
        if (Health <= 0)
        {
            wallActive = false;
        }
    }

    /* IEnumerator CheckWallStatus()
    {
        while(!wallActive)
        {
            
            yield return new WaitForSeconds(.33f);
        }
   } */


}
