using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Interactables
{
    [RequireComponent(typeof(CoffeeMachine))]
    public class CoffeePrepare : MonoBehaviour
    {
        [SerializeField] private GameObject coffeePouringEffect;
        [SerializeField] private GameObject waterPouringEffect;
        [SerializeField] private float coffeePouringDuration = 2f;
        
        private CoffeeMachine _coffeeMachine;
        private Coroutine _pouringCoroutine;
        
        private void OnEnable()
        {
            _coffeeMachine = GetComponent<CoffeeMachine>();
        }

        public void StartCoffeePouring()
        {
            if (_pouringCoroutine != null)
            {
                StopCoroutine(_pouringCoroutine);
            }
            
            _pouringCoroutine = StartCoroutine(CoffeePouring());
        }

        private bool IsCapsulePersist()
        {
            return _coffeeMachine.IsCapsulePlaceEmpty == false;
        }

        private IEnumerator CoffeePouring()
        {
            SetWaterOrCoffeePouring(IsCapsulePersist());

            if (_coffeeMachine.CurrentCup != null)
            {
                _coffeeMachine.CurrentCup.SetStatus(this);
                yield return new WaitForSeconds(coffeePouringDuration);
                DisablePouringEffect();
                var status = IsCapsulePersist() ? CupStatus.Ready : CupStatus.Water;

                if (_coffeeMachine.CurrentCup != null)
                {
                    _coffeeMachine.CurrentCup.SetStatus(this, status);
                }
            }
            else
            {
                yield return new WaitForSeconds(coffeePouringDuration);
                DisablePouringEffect();
            }
            
            _coffeeMachine.UseCapsuleIfPersist(this);
        }

        private void SetWaterOrCoffeePouring(bool isCapsulePersist)
        {
            coffeePouringEffect.SetActive(isCapsulePersist);
            waterPouringEffect.SetActive(isCapsulePersist == false);
        }

        private void DisablePouringEffect()
        {
            coffeePouringEffect.SetActive(false);
            waterPouringEffect.SetActive(false);
        }
    }
}