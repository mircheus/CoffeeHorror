using UnityEngine;

namespace Game.Scripts.Customers
{
    [CreateAssetMenu(menuName = "Create Order", fileName = "Order", order = 0)]
    public class Order : ScriptableObject
    {
        [SerializeField] private string orderName;
        
        public string OrderName => orderName;
    }
}