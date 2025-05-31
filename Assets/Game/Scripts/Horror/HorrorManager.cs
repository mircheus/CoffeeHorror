using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Horror
{
    public class HorrorManager : MonoBehaviour
    {   
        [Header("Horror Components: ")]
        [SerializeField] private HorrorTrigger horrorTrigger;
        [SerializeField] private HorrorNpc horrorNpc;
        [SerializeField] private HorrorLighting horrorLighting;
        [SerializeField] private LookTrigger lookTrigger;
        [SerializeField] private Transform monster;
        
        [Header("References: ")]
        [SerializeField] private Player.Player player;
        [SerializeField] private Transform playerInitialPosition;
        [SerializeField] private Transform npcInitialPosition;

        private void OnEnable()
        {
            horrorTrigger.enabled = false;
            horrorNpc.Customer.OrderCompleted += OnOrderCompleted;
            horrorTrigger.HorrorTriggered += OnHorrorTriggered;
        }
        
        private void OnDisable()
        {
            horrorNpc.Customer.OrderCompleted -= OnOrderCompleted;
            horrorTrigger.HorrorTriggered -= OnHorrorTriggered;
            
        }

        private void OnHorrorTriggered()
        {
            horrorLighting.StartPulsingLight();
            horrorNpc.Customer.ToldOrder += OnCustomerToldOrder;
        }

        private void OnOrderCompleted()
        {
            horrorTrigger.ActivateTrigger(this);
        }

        private void OnCustomerToldOrder(string obj)
        {
            TeleportToInitPosition();
            horrorLighting.StopPulsingLight();
            horrorNpc.Customer.ToldOrder -= OnCustomerToldOrder;
            lookTrigger.enabled = true;
            lookTrigger.LookTriggered += OnLookTriggered;
        }

        private void OnLookTriggered()
        {
            var lookToPlayer = Quaternion.LookRotation(player.transform.position - monster.transform.position, Vector3.up);
            monster.transform.rotation = new Quaternion(monster.transform.rotation.x, lookToPlayer.y, monster.transform.rotation.z, monster.transform.rotation.w);
            var newPos = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            monster.DOMove(newPos, 0.4f);
        }

        private void TeleportToInitPosition()
        {
            if (player != null)
            {
                player.TeleportToInitPosition();
                // player.transform.rotation = playerInitialPosition.rotation;
                horrorNpc.Customer.transform.position = npcInitialPosition.position;
                horrorNpc.Customer.transform.rotation = npcInitialPosition.rotation;
                Debug.Log("Player teleported to initial position.");
            }
            else
            {
                Debug.LogWarning("Player reference is null, cannot teleport.");
            }
        }
    }
}