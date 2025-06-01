using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Horror
{
    public class LookTrigger : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float dotProductThreshold = 0.5f;

        public event UnityAction LookTriggered;

        private void Update()
        {
            CheckLookTrigger();
        }

        private void CheckLookTrigger()
        {
            if (target == null)
            {
                Debug.LogWarning("Target is not set for LookTrigger.");
                return;
            }

            CalculateDotProduct(transform, target, out float dotProduct);

            if (dotProduct >= dotProductThreshold)
            {
                LookTriggered?.Invoke();
            }
        }

        private void CalculateDotProduct(Transform source, Transform target, out float dotProduct)
        {
            Vector3 directionToTarget = (source.position - target.position).normalized;
            dotProduct = Vector3.Dot(target.forward.normalized, directionToTarget);
        }
    }
}