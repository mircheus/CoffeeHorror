using System;
using UnityEngine;

namespace Game.Scripts.Horror
{
    [RequireComponent(typeof(BoxCollider))]
    public class HorrorTrigger : MonoBehaviour
    {
        private bool _isTriggerActive;
        
        public event Action HorrorTriggered;
        
        public void ActivateTrigger(HorrorManager horrorManager)
        {
            _isTriggerActive = true;
        }
        
        public void DeactivateTrigger(HorrorManager horrorManager)
        {
            _isTriggerActive = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggerActive)
            {
                if (other.TryGetComponent(out HorrorNpc npcHorror))
                {
                    npcHorror.TriggerHorrorAction();
                    HorrorTriggered?.Invoke();
                    Debug.Log("Npc entered horror trigger.");
                }
            }
        }
    }
}
