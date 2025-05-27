namespace Game.Prefabs.Interactables
{
    public interface IInteractable
    {
        /// <summary>
        /// Method to be called when the interactable is interacted with.
        /// </summary>
        void Interact();

        /// <summary>
        /// Method to check if the interactable can be interacted with.
        /// </summary>
        /// <returns>True if it can be interacted with, otherwise false.</returns>
        bool CanInteract();
    }
}

