using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public abstract class Trigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_eventOnTrigger;

        private bool isTriggered;
        public bool IsTriggered => isTriggered;

        private void Start()
        {
            isTriggered = false;
        }

        protected virtual void OnTrigger()
        {
            if (isTriggered) return;

            m_eventOnTrigger?.Invoke();
            isTriggered = true;
        }
    }
}
