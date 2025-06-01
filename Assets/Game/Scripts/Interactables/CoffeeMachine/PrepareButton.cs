using System;
using Game.Prefabs.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Interactables
{
    [RequireComponent(typeof(BoxCollider))]
    public class PrepareButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactionText = "Prepare Coffee";
        public event Action StartPrepare;
        public string InteractionText => interactionText;
        
        public void Interact(PlayerInteraction interactor)
        {
            if (interactor == null)
            {
                Debug.LogWarning("PlayerInteraction is null, cannot interact with PrepareButton.");
                return;
            }
            
            StartPrepare?.Invoke();
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            return true;
        }
    }
}