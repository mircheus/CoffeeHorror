using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class CoffeeMachine : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform cupPlace;
        
        private bool _isPlaceOccupied;
        
        public void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with CoffeeMachine.");
                return;
            }

            if (interactor.IsHolding && _isPlaceOccupied == false)
            {
                if (interactor.HeldObject.TryGetComponent(out Cup cup))
                {
                    interactor.ReleaseHeldObject();
                    cup.Drop();
                    cup.FixPosition();
                    cup.Grabbed += OnGrabbed;
                    PlaceOnPosition(cup);
                }
            }
        }

        public bool CanInteract()
        {
            return _isPlaceOccupied == false;
        }

        private void OnGrabbed()
        {
            _isPlaceOccupied = false;
        }

        private void PlaceOnPosition(Cup cup)
        {
            Transform cupTransform;
            (cupTransform = cup.transform).SetParent(transform);
            cupTransform.position = cupPlace.position;
            cupTransform.rotation = Quaternion.identity;
            _isPlaceOccupied = true;
        }
    }
}
