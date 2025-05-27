using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Cup : MonoBehaviour, IInteractable, IDroppable
    {
        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX;
            
            if (_rigidbody == null)
            {
                Debug.LogError("Cup requires a Rigidbody component.");
            }
        }

        public void Interact(PlayerInteraction playerInteraction)
        {
            if (playerInteraction == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with Cup.");
                return;
            }

            _rigidbody.useGravity = false;
            _rigidbody.detectCollisions = true;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.linearVelocity = Vector3.zero;
            playerInteraction.GetCup(this);
        }

        public bool CanInteract()
        {
            return true;
        }
        
        public void Drop(PlayerInteraction playerInteraction)
        {
            playerInteraction.DropCup();
            _rigidbody.useGravity = true;
            transform.rotation = new Quaternion(0, playerInteraction.transform.rotation.y, 0, 1);
            // throw new NotImplementedException();
        }
    }
}