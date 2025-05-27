using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class CoffeeMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform cupPlace;
        
        private bool _isPlaceEmpty;
        private Cup _currentCup;

        private void OnEnable()
        {
            _isPlaceEmpty = true;
        }

        public void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with CoffeeMachine.");
                return;
            }
            
            if (interactor.HeldObject.TryGetComponent(out Cup cup))
            {
                interactor.ReleaseHeldObject();
                _currentCup = cup;
                _currentCup.Drop();
                _currentCup.FixPosition();
                PlaceOnPosition(_currentCup);
                _currentCup.Grabbed += OnGrabbed;
            }
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            return _isPlaceEmpty && interactor.IsHolding;
        }

        private void OnGrabbed()
        {
            _isPlaceEmpty = true;
            _currentCup.Grabbed -= OnGrabbed;
            _currentCup = null;
        }

        private void PlaceOnPosition(Cup cup)
        {
            Transform cupTransform;
            (cupTransform = cup.transform).SetParent(transform);
            cupTransform.position = cupPlace.position;
            cupTransform.rotation = Quaternion.identity;
            _isPlaceEmpty = false;
        }
    }
}