using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private CanvasGroup endScreenCanvasGroup;
        [SerializeField] private float fadeInDuration = 1.5f;

        private void OnEnable()
        {
            if (fadeImage == null)
            {
                Debug.LogError("Fade Image is not assigned in the Fader component.");
                return;
            }

            FadeIn(fadeInDuration);
            // Start fully opaque
        }

        public void FadeIn(float duration)
        {
            if (fadeImage == null) return;
            
            fadeImage.color = new Color(0, 0, 0, 1);
            fadeImage.DOFade(0, duration);
        }

        public void FadeOut(float duration)
        {
            fadeImage.DOFade(1, duration).OnComplete(() =>
                endScreenCanvasGroup.DOFade(1f, 2f));
        }
    }
}