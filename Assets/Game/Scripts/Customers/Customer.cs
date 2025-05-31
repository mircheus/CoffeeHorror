using System;
using System.Collections;
using System.Collections.Generic;
using Game.Prefabs.Interactables;
using Game.Scripts.Horror;
using Game.Scripts.Interactables;
using Game.Scripts.Player;
using UnityEngine;

namespace Game.Scripts.Customers
{
    public class Customer : MonoBehaviour, IInteractable
    {
        [Header("References: ")]
        [SerializeField] private Order order;
        [SerializeField] private List<Transform> waypoints;
        [SerializeField] private List<Transform> reverseWaypoints;
        [SerializeField] private Animator animator;

        [Header("Settings: ")]
        [SerializeField] private float speed;
        [SerializeField] private bool isLooping;
        [SerializeField] private float rotationSpeed = 120f;
        [SerializeField] private float delayBeforeFollowing = 2f;
        
        private int _currentWaypointIndex = 0;
        private int _reached = Animator.StringToHash("Reached");
        private int _following = Animator.StringToHash("Following");
        private PathFollower _pathFollower;
        private bool _isReached = false;
        private Coroutine _startFollowingCoroutine;

        public Order Order => order;
        public event Action<string> ToldOrder;
        public event Action OrderCompleted;
        
        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            _pathFollower.ReachedWaypoint -= OnReachedWaypoint;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cup cup))
            {
                switch (cup.CupStatus)
                {
                    case CupStatus.Ready:
                        OrderCompleted?.Invoke();
                        ToldOrder?.Invoke("Thanks a lot! Have a nice evening!");
                        if (_startFollowingCoroutine != null)
                        {
                            StopCoroutine(_startFollowingCoroutine);
                        }
                        
                        _startFollowingCoroutine = StartCoroutine(StartFollowingWithDelay(delayBeforeFollowing));
                        break;
                    
                    case CupStatus.NotReady:
                        ToldOrder?.Invoke("Cmon man, it's not ready yet!");
                        break;
                    
                    default:
                        ToldOrder?.Invoke("What is this? I didn't order this!");
                        break;
                }
                
                cup.gameObject.SetActive(false);
            }
        }

        public void Init()
        {
            _pathFollower = new PathFollower(waypoints, transform, speed, isLooping, rotationSpeed);
            _pathFollower.ReachedWaypoint += OnReachedWaypoint;
        }

        private void Update()
        {
            if (waypoints == null || waypoints.Count == 0)
                return;

            if (_isReached == false)
            {
                _pathFollower.MoveToWaypoint();
            }
        }

        public void Interact(PlayerInteraction interactor)
        {
            interactor.TakeOrderFrom(this);
            ToldOrder?.Invoke(order.OrderName);
        }

        public void HorrorAction(HorrorNpc horrorNpc)
        {
            _isReached = true;
            animator.SetTrigger(_reached);
        }

        public bool CanInteract(PlayerInteraction interactor)
        {
            if (_isReached == false)
            {
                return false;
            }

            return true;
        }

        private void OnReachedWaypoint()
        {
            _isReached = true;
            animator.SetTrigger(_reached);
        }

        private IEnumerator StartFollowingWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            animator.SetTrigger(_following);
            _pathFollower.SetNewWaypoints(reverseWaypoints);
            _isReached = false;
        }
    }
}