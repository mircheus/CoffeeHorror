using TMPro;
using UnityEngine;

namespace Game.Scripts
{
    public class DamagePopUpGenerator : MonoBehaviour
    {
        [SerializeField] private float duration;
        
        public static DamagePopUpGenerator current;
        public GameObject prefab;

        private void Awake()
        {
            current = this; // TODO: мб избавиться от синглтона
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F10))
            {
                var resultString = Random.Range(1, 4).ToString() + "$";
                CreatePopUp(transform.position, resultString, Color.green);
            }
        }

        public void CreatePopUp(Vector3 position, string text, Color color)
        {
            var popup = Instantiate(prefab, position, Quaternion.identity);
            var temp = popup.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            temp.text = text;
            // temp.faceColor = color;

            //Destroy Timer
            Destroy(popup, duration);
        }
        
        public void CreatePopUp(string text, bool isRandomized)
        {
            CreatePopUp(transform.position + new Vector3(-4, 6, 12), isRandomized ? text : Random.Range(300, 600).ToString(), Color.red);
        }

        public void CreatePopUpDefault(Vector3 position)
        {
            CreatePopUp(position, Random.Range(300, 600).ToString(), Color.yellow);
        }
    }
}
