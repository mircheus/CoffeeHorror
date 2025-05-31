using Game.Scripts.Customers;
using UnityEngine;

namespace Game.Scripts.Horror
{
    public class HorrorNpc : MonoBehaviour
    {
        [SerializeField] private Customer customer;
        
        public Customer Customer => customer;

        public void TriggerHorrorAction()
        {
            customer.HorrorAction(this);
        }
    }
}