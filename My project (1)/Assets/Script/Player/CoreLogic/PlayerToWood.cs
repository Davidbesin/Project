using UnityEngine;

public class PlayerToWood : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]Animator axe;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Forest forest = other.GetComponent<Forest>();
        if (forest == null) return;
        Debug.Log("Tarzan");
      
        axe.SetTrigger("Blink");
    }
    void OnTriggerExit(Collider other)
    {
        Forest forest = other.GetComponent<Forest>();
        if (forest == null) return;
       
        axe.SetTrigger("Still");
    }
}
