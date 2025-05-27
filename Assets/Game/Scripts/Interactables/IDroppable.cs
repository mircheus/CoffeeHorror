using Game.Scripts.Player;

namespace Game.Scripts.Interactables
{
    public interface IDroppable
    {
        void Drop(PlayerInteraction playerInteraction);
    }
}