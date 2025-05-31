using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Customers
{
    public class PathFollower
    {
        private List<Transform> _waypoints = new List<Transform>();
        private Transform _transform;
        private float _speed;
        private bool _isLooping;
        private float _rotationSpeed;
        private int _currentWaypointIndex;
        
        public event UnityAction ReachedWaypoint;

        public PathFollower(List<Transform> waypoints, Transform transform, float speed = 1f, bool isLooping = false, float rotationSpeed = 120f)
        {
            _waypoints = waypoints;
            _transform = transform;
            _speed = speed;
            _isLooping = isLooping;
            _rotationSpeed = rotationSpeed;
            _currentWaypointIndex = 0;
        }

        public void MoveToWaypoint(bool isReverse = false)
        {
            Vector3 npcPos = new Vector3(_transform.position.x, 0, _transform.position.z);
            Vector3 targetPos = new Vector3(_waypoints[_currentWaypointIndex].position.x, 0, _waypoints[_currentWaypointIndex].position.z);
            Vector3 direction = (targetPos - npcPos).normalized;
            Vector3 move = direction * _speed * Time.deltaTime;
            _transform.position += new Vector3(move.x, 0, move.z);
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _transform.rotation = Quaternion.RotateTowards(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            
            float distance = Vector3.Distance(npcPos, targetPos);
            
            if (distance < 0.1f)
            {
                SetNextWaypoint();
            }
        }
        
        public void SetNewWaypoints(List<Transform> newWaypoints)
        {
            _waypoints = newWaypoints;
            _currentWaypointIndex = 0;
        }

        private void SetNextWaypoint(bool isReverse = false)
        {
            _currentWaypointIndex++;

            if (_currentWaypointIndex >= _waypoints.Count)
            {
                if (_isLooping)
                {
                    _currentWaypointIndex = 0;
                }
                else
                {
                    ReachedWaypoint?.Invoke();
                }
            }
        }
    }
}