using Game.Prefabs.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
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
            if (context.performed)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    
                    if (interactable != null && interactable.CanInteract())
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
