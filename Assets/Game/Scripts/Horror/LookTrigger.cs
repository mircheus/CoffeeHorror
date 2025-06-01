using System;
using Game.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Horror
{
    public class LookTrigger : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float dotProductThreshold = 0.5f;
        [SerializeField] private Fader fader;

        public event UnityAction LookTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Player.Player player))
            {
                fader.FadeOut(.5f);
            }
        }

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