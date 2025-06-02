using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fpsText;
        [SerializeField] private float updateInterval = 0.5f;

        private float _timer;
        private int _frames;

        private void Update()
        {
            _frames++;
            _timer += Time.unscaledDeltaTime;

            if (_timer >= updateInterval)
            {
                float fps = _frames / _timer;
                fpsText.text = (Mathf.RoundToInt(fps)).ToString();
                _frames = 0;
                _timer = 0f;
            }
        }
    }
}