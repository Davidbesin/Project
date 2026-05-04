using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
   public void SnapLeft()
    {
     
        transform.Rotate(0f, -45f, 0f, Space.Self);
    }

    public void SnapRight()
    {  
      
        transform.Rotate(0f, 45f, 0f, Space.Self);
    } // Start is called once before the first execution of Update after the MonoBehaviour is created
    
}
