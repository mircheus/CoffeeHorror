using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Cup : MonoBehaviour, IInteractable, IDroppable
    {
        private Rigidbody _rigidbody;
        private bool _isHeld;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            
            if (_rigidbody == null)
            {
                Debug.LogError("Cup requires a Rigidbody component.");
            }
        }

        public void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with Cup.");
                return;
            }

            _isHeld = true;
            _rigidbody.isKinematic = true;
            interactor.SetHeldObject(gameObject);
        }

        public bool CanInteract()
        {
            return true;
        }

        public void Drop()
        {
            _isHeld = false;
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
        }
    }
}