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
    
        private GameObject _currentInteractable;
        private IDroppable _currentDroppable;
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
        }

        private void Update()
        {
            HoldInteractableIfNeeded();
        }

        public void GetCup(Cup cup)
        {
            if (cup != null)
            {
                var cupGameObject = cup.gameObject;
                _currentInteractable = cupGameObject;
                _currentDroppable = cup;
                MoveObjectToHandlePosition(cupGameObject, _player.HandleTransform);
            }
            else
            {
                Debug.LogWarning("Cup is null, cannot acquire.");
            }
        }

        public void DropCup()
        {
            _currentInteractable.transform.SetParent(null, true);
            _currentInteractable = null;
        }

        private void Interact(InputAction.CallbackContext context)
        {
            if (_currentInteractable == null)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(Camera.main.transform.position, _player.CameraHeadForward, out hit, rayDistance))
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
                DropCurrentInteractable();
            }
        }

        private void MoveObjectToHandlePosition(GameObject currentInteractable, Transform handlePosition)
        {
            currentInteractable.gameObject.transform.SetParent(handlePosition, false);
            currentInteractable.gameObject.transform.position = handlePosition.position;
        }

        private void HoldInteractableIfNeeded()
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.transform.position = _player.HandleTransform.position;
                _currentInteractable.transform.rotation = _player.HandleTransform.rotation;
            }
        }

        private void DropCurrentInteractable()
        {
            _currentDroppable?.Drop(this);
        }
    }
}