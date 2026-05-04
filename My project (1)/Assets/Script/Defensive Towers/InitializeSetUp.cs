using UnityEngine;

public class InitializeSetUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        transform.position = Player.Instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
