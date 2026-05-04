using UnityEngine;

public class BillBoardCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private void Update()
    {
        if (mainCamera == null) return;

        // Make the transform face the camera
        transform.LookAt(
            transform.position + mainCamera.transform.rotation * Vector3.forward,
            mainCamera.transform.rotation * Vector3.up
        );

        // Clamp rotation on X axis (keep upright)
        Vector3 euler = transform.eulerAngles;
        euler.x = 0f; // lock X axis
        transform.eulerAngles = euler;
    }
}
