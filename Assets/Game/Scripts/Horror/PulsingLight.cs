using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Horror
{
    [RequireComponent(typeof(Light))]
    public class PulsingLight : MonoBehaviour
    {
        private float _minIntensity = 0f;
        private float _maxIntensity = 2f;
        private float _duration = 1f;
        private Ease _easeType = Ease.InOutSine;

        private Light _lightComponent;
        private Tween _pulseTween;
        private Tween _colorTween;
        private Color _initialColor;

        private void Start()
        {
            _lightComponent = GetComponent<Light>();
            _initialColor = _lightComponent.color;
        }

        public void Init(bool randomized, float minIntensity, float maxIntensity, float duration, Ease easeType = Ease.InOutSine)
        {
            if (randomized)
            {
                _maxIntensity = Random.Range(0.2f, maxIntensity);
                _minIntensity = Random.Range(0f, _maxIntensity);
                _duration = Random.Range(0.2f, duration);
                _easeType = easeType;
            }
            else
            {
                _minIntensity = minIntensity;
                _maxIntensity = maxIntensity;
                _duration = duration;
                _easeType = easeType;
            }
        }

        public void StartPulsing()
        {
            _pulseTween.Kill();
            // Create a looping tween to pulse the intensity
            _pulseTween = DOTween.To(
                    () => _lightComponent.intensity,
                    x => _lightComponent.intensity = x,
                    _maxIntensity,
                    _duration
                )
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_easeType);
        }
        
        public void StartColorChange(Color targetColor, float duration)
        {
            _colorTween.Kill();
            // Create a tween to change the light color
            _colorTween = DOTween.To(
                    () => _lightComponent.color,
                    x => _lightComponent.color = x,
                    targetColor,
                    duration
                )
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(_easeType);
        }

        private void OnDisable()
        {
            // Kill tween when the object is disabled to avoid memory leaks
            _pulseTween?.Kill();
            _colorTween?.Kill();
        }
    }
}