using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Interactables
{
    public class Cup : BaseHoldable
    {
        [SerializeField] private GameObject coffeeMesh;
        [SerializeField] private GameObject waterMesh;
        [SerializeField] private Transform cupCoverPlace;
        
        private CupStatus _cupStatus = CupStatus.Empty;
        private CupCover _currentCupCover;
        private bool _isCupCoverPlaced;
        
        public CupStatus CupStatus => _cupStatus;

        public event UnityAction Grabbed;

        protected override void OnEnable()
        {
            base.OnEnable();
            _isCupCoverPlaced = false;
            coffeeMesh.SetActive(false);
            waterMesh.SetActive(false);
        }

        public override void Interact(PlayerInteraction interactor)
        {
            base.OnEnable();
            
            if(interactor.IsHolding && interactor.HeldObject.TryGetComponent(out CupCover cupCover) && !_isCupCoverPlaced)
            {
                interactor.ReleaseHeldObject();
                _currentCupCover = cupCover;
                _currentCupCover.Drop();
                _currentCupCover.SetKinematicTrue();
                PlaceOnPosition(_currentCupCover);
            }
            else if (interactor.IsHolding == false)
            {
                _isHeld = true;
                _rigidbody.isKinematic = true;
                GameObject cup;
                (cup = gameObject).layer = LayerMask.NameToLayer(Constants.HeldObjectLayer);
                interactor.SetHeldObject(cup);
                Grabbed?.Invoke();
            }
        }

        public override bool CanInteract(PlayerInteraction interactor)
        {
            if (interactor.IsHolding && interactor.HeldObject.TryGetComponent(out CupCover cupCover) && !_isCupCoverPlaced)
            {
                return true;
            }
            
            if(interactor.IsHolding == false)
            {
                return true;
            }
            
            return false;
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
        }

        public void Throw(Vector3 force)
        {
            _isHeld = false;

            transform.SetParent(null);
            _rigidbody.isKinematic = false;

            gameObject.layer = _originalLayer;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddForce(force, ForceMode.Impulse);
        }
        
        private void PlaceOnPosition(CupCover cupCover)
        {
            cupCover.gameObject.GetComponent<BoxCollider>().enabled = false; // TODO: найти другой способ избежать давления крышки на стаканчик
            Transform cupCoverTransform;
            (cupCoverTransform = cupCover.transform).SetParent(cupCoverPlace.transform);
            cupCoverTransform.position = cupCoverPlace.position;
            cupCoverTransform.rotation = cupCoverPlace.rotation;
            _isCupCoverPlaced = true;
        }
    }
}