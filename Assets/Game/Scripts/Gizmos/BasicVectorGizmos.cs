using System;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Player
{
    public class BasicVectorGizmos : MonoBehaviour
    {
        [SerializeField] private bool showForward = true;
        [SerializeField] private bool showRight = true;
        [SerializeField] private bool showUp = true;
        [SerializeField] private Color forwardColor = Color.blue;
        [SerializeField] private Color rightColor = Color.red;
        [SerializeField] private Color upColor = Color.green;
        
        private void OnDrawGizmos()
        {
            DrawBasicVectors();
        }

        private void DrawBasicVectors()
        {
            if (showForward)
            {
                Handles.color = forwardColor;
                Handles.DrawLine(transform.position, transform.position + transform.forward * 2f, 6f);
            }

            if (showRight)
            {
                Handles.color = rightColor;
                Handles.DrawLine(transform.position, transform.position + transform.right * 2f, 6f);
            }


            if (showUp)
            {
                Handles.color = upColor;
                Handles.DrawLine(transform.position, transform.position + transform.up * 2f, 6f);
            }
        }
    }
}