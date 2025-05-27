using System;
using UnityEngine;

namespace Game.Scripts.Player
{
    [RequireComponent(typeof(PlayerInteraction))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject cameraHead;
        [SerializeField] private GameObject handlePosition;

        private PlayerInteraction _playerInteraction;
        
        public Vector3 CameraHeadForward => cameraHead.transform.forward;
        public Vector3 CameraHeadUp => cameraHead.transform.up;
        public Vector3 CameraHeadPosition => cameraHead.transform.position;
        public Transform HandleTransform => handlePosition.transform;

        private void Start()
        {
            InitPlayerInteraction();
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
    }
}