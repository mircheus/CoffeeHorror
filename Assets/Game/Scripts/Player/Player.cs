using UnityEngine;

namespace Game.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private GameObject cameraHead;
        
        public Vector3 CameraHeadForward => cameraHead.transform.forward;
    }
}