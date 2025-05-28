using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("References: ")]
        [SerializeField] private DialogueMediator mediator;
        [SerializeField] private CanvasGroup textPanel;
        [SerializeField] private TMP_Text dialogueText;

        [Header("Settings: ")]
        [SerializeField] private float hideDelay = 4f; // Delay before hiding the dialogue
        
        private IEnumerator _hideCoroutine;
        
        private void OnEnable()
        {
            mediator.CustomerToldOrder += ShowDialogue;
            dialogueText.text = string.Empty; // Clear dialogue text on enable

            if (textPanel == null)
            {
                Debug.LogWarning("TextPanel is not assigned in the DialogueUI component. Disabling dialogue UI.");
                enabled = false;
            }

            textPanel.alpha = 0;
        }

        private void OnDisable()
        {
            mediator.CustomerToldOrder -= ShowDialogue;
        }

        private void ShowDialogue(string orderName)
        {
            textPanel.DOFade(1, 0.25f).OnComplete(() =>
            {
                SetText(orderName);
                HideDialogue();
            });
        }

        private void SetText(string text)
        {
            if (dialogueText != null)
                dialogueText.text = text;
            else
                Debug.LogWarning("Dialogue Text is not assigned in the DialogueUI component.");
        }
        
        private void HideDialogue()
        {
            if (_hideCoroutine != null)
            {
                StopCoroutine(_hideCoroutine);
            }
            
            _hideCoroutine = HideWithDelay(hideDelay);
            StartCoroutine(_hideCoroutine);
        }
        
        private IEnumerator HideWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            textPanel.DOFade(0, 0.25f).OnComplete(() =>
            {
                dialogueText.text = string.Empty; // Clear text after hiding
            });
        }
    }
}
