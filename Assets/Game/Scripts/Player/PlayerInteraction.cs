using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private InputActionReference interactAction;
        [SerializeField] private float rayDistance = 3f;
        
        private HoldableHandler _holdableHandler;
        private Player _player;

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

        public void Init(Player player)
        {
            _player = player;
            _holdableHandler = new HoldableHandler(_player.HoldPoint);
        }

        public void SetHeldObject(GameObject obj)
        {
            if (_holdableHandler.IsHolding)
            {
                Debug.LogWarning("Already holding an object, cannot set a new one.");
                return;
            }

            _holdableHandler.SetHeldObject(obj);
        }
        
        private void Interact(InputAction.CallbackContext context)
        {
            if (_holdableHandler.IsHolding == false)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(_player.CameraHeadPosition, _player.CameraHeadForward, out hit, rayDistance))
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    
                    if (interactable != null && interactable.CanInteract())
                    {
                        interactable.Interact(this);
                    }
                }  
            }
            else
            {
                _holdableHandler.DropHeldObject();
            }
        }
    }
}