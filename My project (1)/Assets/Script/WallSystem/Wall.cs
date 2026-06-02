using UnityEngine;

public class Wall : MonoBehaviour, IHealth
{
    MeshRenderer mesh;
    BoxCollider collider;

    [SerializeField] private int health = 100;

    public string HealthText => $"HP: {Health}";

    public bool PlayerSide => true;

    bool wallActive = true;
    public bool WallActive
    {
        get => wallActive;
        set
        {
            wallActive = value;

            mesh.enabled = value;
            collider.enabled = value;
        }
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            WallActive = false;
        }
    }
}