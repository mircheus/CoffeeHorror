using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Customers;
using Game.Scripts.Interactables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private InputActionReference interactAction;
        [SerializeField] private InputActionReference throwAction;
        [Header("Settings: ")]
        [SerializeField] private float rayDistance = 3f;
        [SerializeField] private float throwForce = 5f;
        [SerializeField] private LayerMask interactLayer = ~0; 
        
        private HoldableHandler _holdableHandler;
        private Player _player;

        public bool IsHolding => _holdableHandler.IsHolding;
        public GameObject HeldObject => _holdableHandler.HeldObject;
        
        private void OnEnable()
        {
            interactAction.action.Enable();
            throwAction.action.Enable();
            interactAction.action.performed += TryInteract;
            throwAction.action.performed += TryThrow;
        }

        private void OnDisable()
        {
            interactAction.action.Disable();
            throwAction.action.Disable();
            interactAction.action.performed -= TryInteract;
            throwAction.action.performed -= TryThrow;
        }

        public void Init(Player player)
        {
            _player = player;
            _holdableHandler = new HoldableHandler(_player.HoldPoint);
        }

        public void TakeOrderFrom(Customer customer)
        {
            Debug.Log("PlayerInteracted");
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

        public void ReleaseHeldObject()
        {
            if (_holdableHandler.IsHolding == false)
            {
                Debug.LogWarning("Not holding any object to release.");
                return;
            }

            _holdableHandler.DropHeldObject();
        }

        private void TryThrow(InputAction.CallbackContext obj)
        {
            if (_holdableHandler.IsHolding)
            {
                var throwObject = _holdableHandler.HeldObject;
                _holdableHandler.DropHeldObject();
                Throw(throwObject);
            }
        }

        private void TryInteract(InputAction.CallbackContext context)
        {
            RaycastHit hit;
                                                                                                                                            
            if (Physics.Raycast(_player.CameraHeadPosition, _player.CameraHeadForward, out hit, rayDistance, interactLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();

                if (interactable != null && interactable.CanInteract(this))
                {
                    interactable.Interact(this);
                }
            }
            else
            {
                _holdableHandler.DropHeldObject();
            }
        }

        private void Throw(GameObject heldObject) 
        {
            Vector3 throwDirection = _player.CameraHeadForward;
            Vector3 force = throwDirection * throwForce;
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}