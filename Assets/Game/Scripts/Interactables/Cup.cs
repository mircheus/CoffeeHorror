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
        [SerializeField] private GameObject coffeeMesh;
        [SerializeField] private GameObject waterMesh;
        
        private CupStatus _cupStatus = CupStatus.Empty;
        private Rigidbody _rigidbody;
        private bool _isHeld;
        private int _originalLayer;
        
        public CoffeeIngredientType IngredientType => ingredientType;
        public CupStatus CupStatus => _cupStatus;

        public event UnityAction Grabbed;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _originalLayer = gameObject.layer;
            coffeeMesh.SetActive(false);
            waterMesh.SetActive(false);

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

        public void SetStatus(CoffeePrepare prepareProcess, CupStatus status = CupStatus.NotReady)
        {
            _cupStatus = status;

            switch (_cupStatus)
            {
                case CupStatus.Ready:
                    coffeeMesh.SetActive(true);
                    break;
                
                case CupStatus.Water:
                    waterMesh.SetActive(true);
                    break;
                
                case CupStatus.NotReady:
                    coffeeMesh.SetActive(false);
                    waterMesh.SetActive(false);
                    break;
            }

            Debug.Log("_cupStatus set to: " + _cupStatus);
        }
    }
}