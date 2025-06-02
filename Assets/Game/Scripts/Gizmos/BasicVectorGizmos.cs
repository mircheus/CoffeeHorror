using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class BasicVectorGizmos : MonoBehaviour
    {
        [Header("Forward")]
        [SerializeField] private bool showForward = true;
        [SerializeField] private Color forwardColor = Color.blue;
        [SerializeField] private float forwardLength = 2f;
        
        [Header("Right")]
        [SerializeField] private bool showRight = true;
        [SerializeField] private Color rightColor = Color.red;
        [SerializeField] private float rightLength = 2f;
        
        [Header("Left")]
        [SerializeField] private bool showUp = true;
        [SerializeField] private Color upColor = Color.green;
        [SerializeField] private float upLength = 2f;
        
        // private void OnDrawGizmos()
        // {
        //     DrawBasicVectors();
        // }
        //
        // private void DrawBasicVectors()
        // {
        //     if (showForward)
        //     {
        //         Handles.color = forwardColor;
        //         Handles.DrawLine(transform.position, transform.position + transform.forward * forwardLength, 6f);
        //     }
        //
        //     if (showRight)
        //     {
        //         Handles.color = rightColor;
        //         Handles.DrawLine(transform.position, transform.position + transform.right * rightLength, 6f);
        //     }
        //
        //
        //     if (showUp)
        //     {
        //         Handles.color = upColor;
        //         Handles.DrawLine(transform.position, transform.position + transform.up * upLength, 6f);
        //     }
        // }
    }
}