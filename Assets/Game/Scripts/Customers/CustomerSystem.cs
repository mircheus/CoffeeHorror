using UnityEngine;

namespace Game.Scripts.Customers
{
    public class CustomerSystem : MonoBehaviour
    {
        [SerializeField] private Customer[] customers;
        [SerializeField] private Transform customerSpawnPoint;
        [SerializeField] private Transform[] pathPoints;
        
        private void StartCustomerPath(Customer customer)
        {
            
        }
    }
}
