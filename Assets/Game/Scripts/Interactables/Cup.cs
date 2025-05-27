using Game.Prefabs.Interactables;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    public class Cup : MonoBehaviour, IInteractable
    {
        public void Interact()
        {
            Debug.Log("This is a cup");
        }

        public bool CanInteract()
        {
            return true;
        }
    }
}