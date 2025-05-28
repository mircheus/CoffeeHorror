using System;
using Game.Scripts.Customers;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.UI
{
    public class ViewUIMediator : MonoBehaviour
    {
        [SerializeField] private Customer customer;

        public event UnityAction<string> CustomerToldOrder;

        private void OnEnable()
        {
            customer.ToldOrder += OnOrderTold;
            customer.OrderCompleted += OnOrderCompleted;
        }
        
        private void OnDisable()
        {
            customer.ToldOrder -= OnOrderTold;
            customer.OrderCompleted -= OnOrderCompleted;
        }

        private void OnOrderTold(string orderName)
        {
            CustomerToldOrder?.Invoke(orderName);
        }

        private void OnOrderCompleted()
        {
            Debug.Log("OnOrderComplete");
            DamagePopUpGenerator.current.CreatePopUp(
                customer.transform.position + Vector3.up * 0.5f, 
                "1.5$", 
                Color.green);
        }
    }
}