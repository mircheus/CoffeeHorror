using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class CoffeeMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform cupPlace;
        [SerializeField] private Transform capsulePlace;
        [SerializeField] private PrepareButton prepareButton;
        [SerializeField] private CoffeePrepare coffeePrepare;
        
        private bool _isCupPlaceEmpty;
        private bool _isCapsulePlaceEmpty;
        private Cup _currentCup;
        private CoffeeCapsule _currentCapsule;
        
        public bool IsCapsulePlaceEmpty => _isCapsulePlaceEmpty;
        public Cup CurrentCup => _currentCup;
        public string InteractionText { get; }

        private void OnEnable()
        {
            _isCupPlaceEmpty = true;
            _isCapsulePlaceEmpty = true;
            prepareButton.StartPrepare += OnPrepareButtonPressed;
        }

        private void OnDisable()
        {
            prepareButton.StartPrepare -= OnPrepareButtonPressed;
        }


        public void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with CoffeeMachine.");
                return;
            }

            if (interactor.IsHolding && interactor.HeldObject.TryGetComponent(out CoffeeCapsule capsule))
            {
                interactor.ReleaseHeldObject();
                _currentCapsule = capsule;
                _currentCapsule.Drop();
                _currentCapsule.SetKinematicTrue();
                _currentCapsule.SetInteractable(this, false);
                PlaceOnPosition(_currentCapsule);
            }
            
            if (interactor.IsHolding && interactor.HeldObject.TryGetComponent(out Cup cup))
            {
                interactor.ReleaseHeldObject();
                _currentCup = cup;
                _currentCup.Drop();
                _currentCup.SetKinematicTrue();
                PlaceOnPosition(_currentCup);
                _currentCup.Grabbed += OnGrabbed;
            }
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            if (interactor.IsHolding)
            {
                if(interactor.HeldObject.TryGetComponent(out CoffeeCapsule capsule))
                {
                    return _isCapsulePlaceEmpty && interactor.IsHolding;
                }
            
                if (interactor.HeldObject.TryGetComponent(out Cup cup))
                {
                    return _isCupPlaceEmpty && interactor.IsHolding;
                }
            }
            
            return false;
        }

        public void UseCapsuleIfPersist(CoffeePrepare coffeePrepare)
        {
            _isCapsulePlaceEmpty = true;
        }

        private void OnGrabbed()
        {
            _isCupPlaceEmpty = true;
            _currentCup.Grabbed -= OnGrabbed;
            _currentCup = null;
        }

        private void PlaceOnPosition(Cup cup)
        {
            Transform cupTransform;
            (cupTransform = cup.transform).SetParent(transform);
            cupTransform.position = cupPlace.position;
            cupTransform.rotation = Quaternion.identity;
            _isCupPlaceEmpty = false;
        }

        private void PlaceOnPosition(CoffeeCapsule capsule)
        {
            Transform capsuleTransform;
            (capsuleTransform = capsule.transform).SetParent(capsulePlace.transform);
            capsuleTransform.position = capsulePlace.position;
            capsuleTransform.rotation = capsulePlace.rotation;
            _isCapsulePlaceEmpty = false;
        }

        private void OnPrepareButtonPressed()
        {
            coffeePrepare.StartCoffeePouring();

            if (_currentCapsule != null)
            {
                Destroy(_currentCapsule.gameObject);
                _currentCapsule = null;
            }
        }
    }
}