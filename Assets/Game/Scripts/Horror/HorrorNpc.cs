using Game.Prefabs.Interactables;
using Game.Scripts.Customers;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Horror
{
    public class HorrorNpc : MonoBehaviour
    {
        [SerializeField] private Customer customer;
        
        public Customer Customer => customer;

        public void TriggerHorrorAction()
        {
            customer.StopMoving(this);
            customer.enabled = false;
        }
    }
}