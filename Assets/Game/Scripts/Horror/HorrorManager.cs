using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.Horror
{
    public class HorrorManager : MonoBehaviour
    {
        [SerializeField] private HorrorTrigger horrorTrigger;
        [SerializeField] private HorrorNpc horrorNpc;
        [SerializeField] private HorrorLighting horrorLighting;

        private void OnEnable()
        {
            horrorTrigger.enabled = false;
            horrorNpc.Customer.OrderCompleted += OnOrderCompleted;
            horrorTrigger.HorrorTriggered += OnHorrorTriggered;
        }
        
        private void OnDisable()
        {
            horrorNpc.Customer.OrderCompleted -= OnOrderCompleted;
            horrorTrigger.HorrorTriggered -= OnHorrorTriggered;
        }

        private void OnHorrorTriggered()
        {
            horrorLighting.StartPulsingLight();
        }

        private void OnOrderCompleted()
        {
            horrorTrigger.ActivateTrigger(this);
        }
    }
}