using System;
using Game.Prefabs.Interactables;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.UI
{
    public class InteractionUI : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private Player.Player player;
        [SerializeField] private TMP_Text showText;
        [SerializeField] private LayerMask interactLayer = ~0; 

        [Header("Raycast Settings")]
        public float interactDistance = 1.5f;

        private IInteractable currentTarget;

        private void Start()
        {
            if (showText != null)
                showText.enabled = false; // Hide on start
        }

        private void Update()
        {
            CheckForInteractable();
        }

        private void CheckForInteractable()
        {
            RaycastHit hit;

            if(Physics.Raycast(player.CameraHeadPosition, player.CameraHeadForward, out hit, interactDistance, interactLayer))
            {
                currentTarget = hit.collider.GetComponent<IInteractable>();

                if (currentTarget != null)
                {
                    if (!showText.enabled)
                    {
                        showText.text = currentTarget.InteractionText ?? "Interact LMB";
                        showText.enabled = true;
                    }
                    
                    return;
                }
            }
            
            currentTarget = null;
            if (showText.enabled)
                showText.enabled = false;
        }
    }
}