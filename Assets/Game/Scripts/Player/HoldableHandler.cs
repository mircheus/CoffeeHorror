using Game.Scripts.Interactables;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class HoldableHandler
    {
        private readonly Transform _holdPoint;
        
        private GameObject _heldObject;

        public bool IsHolding => _heldObject != null;
        public GameObject HeldObject => _heldObject;

        public HoldableHandler(Transform holdPoint)
        {
            _holdPoint = holdPoint;
        }
        
        public void SetHeldObject(GameObject obj)
        {
            if (_heldObject != null) return;

            _heldObject = obj;
            _heldObject.transform.SetParent(_holdPoint);
            _heldObject.transform.localPosition = Vector3.zero;
            _heldObject.transform.localRotation = Quaternion.identity;

            Rigidbody rigidbody = _heldObject.GetComponent<Rigidbody>();
            
            if (rigidbody != null && rigidbody.isKinematic == false)
            {
                rigidbody.isKinematic = true;
            }
        }

        public void DropHeldObject()
        {
            if (_heldObject == null) return;

            if (_heldObject.TryGetComponent<IDroppable>(out var droppable))
            {
                droppable.Drop();
            }

            _heldObject = null;
        }
    }
}