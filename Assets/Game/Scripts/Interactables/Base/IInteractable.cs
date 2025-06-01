using Game.Scripts.Player;

namespace Game.Prefabs.Interactables
{
    public interface IInteractable
    {
        public string InteractionText { get; }
        /// <summary>
        /// Method to be called when the interactable is interacted with.
        /// </summary>
        void Interact(PlayerInteraction interactor);

        /// <summary>
        /// Method to check if the interactable can be interacted with.
        /// </summary>
        /// <returns>True if it can be interacted with, otherwise false.</returns>
        bool CanInteract(PlayerInteraction interactor);
    }
}

