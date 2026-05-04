using UnityEngine;

public interface ICombatant : IHealth
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int GiveDamage(bool side);
}
