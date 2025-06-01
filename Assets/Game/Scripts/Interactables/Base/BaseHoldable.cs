using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public abstract class BaseHoldable : MonoBehaviour, IInteractable, IDroppable
    {
        [SerializeField] protected CoffeeIngredientType ingredientType = CoffeeIngredientType.Cup;
        [SerializeField] protected string interactionText = "Interact LMB";
        
        protected bool _isHeld;
        protected Rigidbody _rigidbody;
        protected int _originalLayer;
        public string InteractionText => interactionText;
        
        public CoffeeIngredientType IngredientType => ingredientType;
        
        protected virtual void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _originalLayer = gameObject.layer;

            if (_rigidbody == null)
            {
                Debug.LogError($"{gameObject.name} requires a Rigidbody component.");
            }
        }

        

        public virtual void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with Cup.");
                return;
            }
            
            _isHeld = true;
            _rigidbody.isKinematic = true;
            gameObject.layer = LayerMask.NameToLayer(Constants.HeldObjectLayer);
            interactor.SetHeldObject(gameObject);
        }
        
        public virtual bool CanInteract(PlayerInteraction interactor)
        {
            return true;
        }

        public virtual void Drop()
        {
            _isHeld = false;
            gameObject.layer = _originalLayer;
            transform.SetParent(null);
            _rigidbody.isKinematic = false;
        }
        
        public virtual void SetKinematicTrue()
        {
            _rigidbody.isKinematic = true;
        }
    }
}