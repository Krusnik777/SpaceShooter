using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class HackingZone : MonoBehaviour
    {
        [SerializeField] private float m_hackingTime;
        [SerializeField] private float m_radius;
        [SerializeField] private SpriteFillBar m_spriteFillBar;
        public UnityEvent HackCompleted;

        private float timer;

        private bool isHacked = false;

        private void Start()
        {
            m_spriteFillBar.SetFillAmountStep(m_hackingTime);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            m_spriteFillBar.gameObject.SetActive(true);

            if (collision.attachedRigidbody == null || isHacked == true) return;

            timer += Time.deltaTime;

            m_spriteFillBar.FillBar();

            if (timer > m_hackingTime)
            {
                isHacked = true;
                HackCompleted?.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            m_spriteFillBar.gameObject.SetActive(false);
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_radius;
        }

#endif

    }
}
