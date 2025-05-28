using UnityEngine;

public class UIBillboarding : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 directionToCamera = cameraPosition - transform.position;
            directionToCamera.y = 0; // Keep the y-axis unchanged
            transform.rotation = Quaternion.LookRotation(-directionToCamera);
        }
    }
}
