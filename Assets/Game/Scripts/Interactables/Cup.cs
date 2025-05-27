using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Interactables
{
    public class Cup : MonoBehaviour, IInteractable, IDroppable
    {
        [SerializeField] protected CoffeeIngredientType ingredientType = CoffeeIngredientType.Cup;
        
        private Rigidbody _rigidbody;
        private bool _isHeld;
        private int _originalLayer;
        
        public CoffeeIngredientType IngredientType => ingredientType;
        
        public event UnityAction Grabbed;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _originalLayer = gameObject.layer;

            if (_rigidbody == null)
            {
                Debug.LogError($"{gameObject.name} requires a Rigidbody component.");
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
            gameObject.layer = LayerMask.NameToLayer(Constants.HeldObjectLayer);
            Grabbed?.Invoke();
            interactor.SetHeldObject(gameObject);
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            if (interactor.IsHolding == false)
            {
                return true;
            }

            return false;
        }

        public void Drop()
        {
            _isHeld = false;
            gameObject.layer = _originalLayer;
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
        }

        public void SetKinematicTrue()
        {
            _rigidbody.isKinematic = true;
        }
    }
}