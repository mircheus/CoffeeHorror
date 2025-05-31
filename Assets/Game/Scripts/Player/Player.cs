using System;
using System.Collections;
using Hertzole.GoldPlayer;
using UnityEngine;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(PlayerInteraction))]
    public class Player : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private GameObject cameraHead;
        [SerializeField] private GameObject holdPoint;

        private PlayerInteraction _playerInteraction;
        private GoldPlayerController _goldPlayerController;
        
        public Vector3 CameraHeadForward => cameraHead.transform.forward;
        public Vector3 CameraHeadUp => cameraHead.transform.up;
        public Vector3 CameraHeadPosition => cameraHead.transform.position;
        public Transform HoldPoint => holdPoint.transform;

        private void Start()
        {
            InitPlayerInteraction();
            _goldPlayerController = GetComponent<GoldPlayerController>();
        }

        private void InitPlayerInteraction()
        {
            if(_playerInteraction == null)
            {
                _playerInteraction = GetComponent<PlayerInteraction>();
                
                if (_playerInteraction != null)
                {
                    _playerInteraction.Init(this);
                }
                else
                {
                    Debug.LogError("PlayerInteraction component is missing on Player.");
                }
            }
        }

        public void TeleportToInitPosition()
        {
            _goldPlayerController.enabled = false;
            transform.position = new Vector3(-0.57f, transform.position.y, -1.55f); // Replace with actual initial position
            transform.rotation = Quaternion.LookRotation(Vector3.zero);
            StartCoroutine(ActivateControllerWithDelay(.5f));
        }
        
        private IEnumerator ActivateControllerWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _goldPlayerController.enabled = true;
        }
    }
}