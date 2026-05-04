using UnityEngine;

public class MineLook : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Mine personalMine;

    void Awake()
    {
        personalMine = GetComponent<Mine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
