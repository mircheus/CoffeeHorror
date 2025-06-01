using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Spawner : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject prefabToSpawn;

        public void Interact(PlayerInteraction interactor)
        {
            var instantiatedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            
            if (instantiatedObject.TryGetComponent(out BaseHoldable holdable))
            {
                holdable.Interact(interactor);
            }
            else
            {
                Debug.LogWarning($"{prefabToSpawn.name} does not implement BaseHoldable, cannot interact.");
            }
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            if (interactor.IsHolding)
            {
                return false;
            }
            
            return true;
        }
    }
}