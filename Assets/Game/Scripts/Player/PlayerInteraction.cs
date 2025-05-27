using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private InputActionReference interactAction;
    
    private void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += Interact;
    }
    
    private void OnDisable()
    {
        interactAction.action.Disable();
        interactAction.action.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted");
    }
}
