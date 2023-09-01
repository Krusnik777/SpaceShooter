using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class FewConditionTrigger : MonoBehaviour
    {
        [SerializeField] private List<Trigger> m_triggers;
        [SerializeField] private UnityEvent m_eventOnTrigger;

        private bool isTriggered;

        private void Update()
        {
            if (!isTriggered && CheckTriggers())
            {
                m_eventOnTrigger?.Invoke();
                isTriggered = true;
            }
        }

        private bool CheckTriggers()
        {
            foreach (var v in m_triggers)
            {
                if (v.IsTriggered == false)
                    return false;
            }
            return true;
        }
    }
}
