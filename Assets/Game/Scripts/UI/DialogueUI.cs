using Game.Scripts.Customers;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private DialogueMediator mediator;
        [SerializeField] private TMP_Text dialogueText;
        
        private void OnEnable()
        {
        }
    }

    public class DialogueMediator : MonoBehaviour
    {
        [SerializeField] private Customer customer;
        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private string[] dialogues;
        
        public void ShowDialogue(int index)
        {
            if (index < 0 || index >= dialogues.Length)
            {
                Debug.LogWarning("Dialogue index out of range.");
                return;
            }
            
            // dialogueUI.dialogueText.text = dialogues[index];
        }
        
    }
}
