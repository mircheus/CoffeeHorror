using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Horror
{
    public class HorrorLighting : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private PulsingLight[] lights;
        
        [Header("Settings: ")]
        [SerializeField] private bool randomized = false; // If true, min/max intensity and duration will be randomized
        [SerializeField] private float minIntensity = 0f;
        [SerializeField] private float maxIntensity = 2f;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease easeType = Ease.InOutSine;
        [SerializeField] private Color targetColor;
        [SerializeField] private float colorChangeDuration = 5f;

        private void Start()
        {
            foreach (var light in lights)
            {
                light.Init(randomized, minIntensity, maxIntensity, duration, easeType);
            }
        }

        private void OnEnable()
        {
            // foreach (var light in lights)
            // {
            //     light.Init(randomized, minIntensity, maxIntensity, duration, easeType);
            // }
            //
            // StartPulsingLight();
        }

        public void StartPulsingLight()
        {
            foreach (var light in lights)
            {
                light.StartPulsing();
                light.StartColorChange(targetColor, colorChangeDuration);
            }
        }
    }
}