using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private bool m_hold;
        public bool IsHold => m_hold;

        public void OnPointerDown(PointerEventData eventData)
        {
            m_hold = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_hold = false;
        }
    }
}
