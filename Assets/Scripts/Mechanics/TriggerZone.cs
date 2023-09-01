using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private UnityEvent m_onTriggerEnter;

        private bool isTriggered;
        public bool IsTriggered => isTriggered;

        private void Start()
        {
            isTriggered = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.root.TryGetComponent(out SpaceShip ship))
            {
                if (Player.Instance.ActiveShip == ship && !isTriggered)
                {
                    m_onTriggerEnter?.Invoke();
                    isTriggered = true;
                }
            }
        }
    }
}
