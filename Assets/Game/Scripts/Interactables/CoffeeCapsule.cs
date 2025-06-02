using Game.Scripts.Player;

namespace Game.Scripts.Interactables
{
    public class CoffeeCapsule : BaseHoldable
    {
        private bool _isInteractable = true;

        public void SetInteractable(CoffeeMachine coffeeMachine, bool isInteractable)
        {
            _isInteractable = isInteractable;
        }
        
        public override bool CanInteract(PlayerInteraction interactor)
        {
            return _isInteractable;
        }
    }
}