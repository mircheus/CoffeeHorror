using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using VolFx;

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
        [SerializeField] private Transform monsterInitPosition;

        [Header("VHS Settings:")] 
        [SerializeField] private Volume vhsVolume;
        
        [Header("References: ")]
        [SerializeField] private Player.Player player;
        [SerializeField] private Transform playerInitialPosition;
        [SerializeField] private Transform npcInitialPosition;

        private VhsVol _vhsVolComponent;
        private float _defaultWeight;
        private float _defaultFlickering;
        private float _defaultRocking;

        private void OnEnable()
        {
            horrorTrigger.enabled = false;
            horrorNpc.Customer.OrderCompleted += OnOrderCompleted;
            horrorTrigger.HorrorTriggered += OnHorrorTriggered;
            lookTrigger.LookTriggered += OnLookTriggered;
            _vhsVolComponent = GetVhsVolumeSettings();
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
            EnableVhsEffect();
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
            monster.gameObject.SetActive(true);
            lookTrigger.enabled = true;
        }

        private void OnLookTriggered()
        {
            var lookToPlayer = Quaternion.LookRotation(player.transform.position - monster.transform.position, Vector3.up);
            monster.transform.rotation = new Quaternion(monster.transform.rotation.x, lookToPlayer.y, monster.transform.rotation.z, monster.transform.rotation.w);
            var newPos = new Vector3(player.transform.position.x, 1, player.transform.position.z);
            monster.DOMove(newPos, 0.5f);
            lookTrigger.enabled = false;
            lookTrigger.LookTriggered -= OnLookTriggered;
        }
        
        private void TeleportToInitPosition()
        {
            if (player != null)
            {
                DisableVhsEffect();
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

        private void EnableVhsEffect()
        {
            // vhsVolume.gameObject.SetActive(true);
            _vhsVolComponent._weight.value = 0.9f;
            _vhsVolComponent._flickering.value = 0.6f;
            _vhsVolComponent._rocking.value = 0.8f;
            // Debug.Log("vhs == null: " + (vhs == null));
            // vhs._weight.value = 1f;
        }

        private void DisableVhsEffect()
        {
            _vhsVolComponent._weight.value = _defaultWeight;
            _vhsVolComponent._flickering.value = _defaultFlickering;
            _vhsVolComponent._rocking.value = _defaultRocking;
        }
        

        private VhsVol GetVhsVolumeSettings()
        {
            // var vhsVol = (VhsVol)vhsVolume.components[0];
            var vhsVol = (VhsVol)vhsVolume.profile.components[0];
            _defaultWeight = vhsVol._weight.value;
            _defaultFlickering = vhsVol._flickering.value;
            _defaultRocking = vhsVol._rocking.value;
            return vhsVol;
        }
    }
}