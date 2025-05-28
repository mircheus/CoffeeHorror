using System;
using Game.Scripts.Customers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.UI
{
    public class DialogueMediator : MonoBehaviour
    {
        [SerializeField] private Customer customer;

        public event UnityAction<string> CustomerToldOrder;

        private void OnEnable()
        {
            customer.ToldOrder += OnOrderTold;
        }
        
        private void OnDisable()
        {
            customer.ToldOrder -= OnOrderTold;
        }

        private void OnOrderTold(string orderName)
        {
            CustomerToldOrder?.Invoke(orderName);
        }
    }
}